using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Position
{
	Left,
	Right,
	Top,
	Bottom,
}

public class Player : MonoBehaviour {
	static float LANE_OFFSET = 2;
	Fire feu;
	Dictionary<Position, Lane> lanes;
	Lane currentLane = null;

	public GameObject spell;    // prefab for a spell
	public float spellSpeed = 10;
	public float cooldown = 0.3f;

	Element[] currentCast = new Element[] { null, null };
	bool canCastSpell = true;

	// Use this for initialization
	void Awake () {
		lanes = new Dictionary<Position, Lane>();
		Lane[] childLanes = GetComponentsInChildren<Lane>();
		foreach (Lane l in childLanes)
		{
			lanes.Add(l.position, l);
		}
		switchLane(Position.Top);
	}
	

	void Update () {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 10;	// Distance entre le joueur et la caméra
		updateLane(Camera.main.ScreenToWorldPoint(mousePos));

		control();
	}

	private void control()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			addElement(new Fire());
		}
		if (Input.GetKeyDown(KeyCode.Z))
		{
			addElement(new Water());
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			addElement(new Mud());
		}

		if (Input.GetMouseButtonDown(0) && canCastSpell)
		{
			castSpell();
		}
		else if (Input.GetMouseButtonDown(1))
		{
			// Cancel
		}
	}

	void resetSpell()
	{
		for(int i=0; i< currentCast.Length; i++)
		{
			currentCast[i] = null;
		}
	}

	void castSpell()
	{
		Element finalElement;
		if (currentCast[0] == null)
		{
			return;
		}
		else
		{
			finalElement = currentCast[0];
			if (currentCast[1] != null)
			{
				finalElement = finalElement.combine(currentCast[1]);
			}
		}
		Debug.Log(currentCast[0].GetType()+" "+currentCast[1].GetType());
		Debug.Log("spell: "+ finalElement.GetType());
		GameObject go = (GameObject)Instantiate(spell, this.transform.position, this.transform.rotation);
		go.GetComponent<Spell>().element = finalElement;
		go.transform.LookAt(currentLane.endLane);
		go.GetComponent<Rigidbody2D>().velocity = go.transform.forward * spellSpeed;

		StartCoroutine(waitCooldown());
	}

	IEnumerator waitCooldown()
	{
		resetSpell();
		canCastSpell = false;
		yield return new WaitForSeconds(cooldown);
		canCastSpell = true;
	}

	void addElement(Element elem)
	{
		if (currentCast[0] == null)
			currentCast[0] = elem;
		else
			currentCast[1] = elem;
	}

	void updateLane(Vector3 mousePosition)
	{
		Vector3 pos = this.transform.position;
		bool isHorizontal = (pos.y + LANE_OFFSET > mousePosition.y && pos.y - LANE_OFFSET < mousePosition.y);
		bool isVertical = (pos.x + LANE_OFFSET > mousePosition.x && pos.x - LANE_OFFSET < mousePosition.x);

		if (isHorizontal)
		{
			if(mousePosition.x > pos.x)
			{
				switchLane(Position.Right);
			}
			else
			{
				switchLane(Position.Left);
			}
		}
		else if(!isHorizontal && isVertical)
		{
			if (mousePosition.y > pos.y)
			{
				switchLane(Position.Top);
			}
			else
			{
				switchLane(Position.Bottom);
			}
		}
	}

	void switchLane(Position pos)
	{
		if (currentLane) currentLane.ActivateLane(false);
		currentLane = lanes[pos];
		currentLane.ActivateLane(true);
	}
}
