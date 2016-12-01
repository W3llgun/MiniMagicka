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

[System.Serializable]
public class ElementSprite
{
	public Sprite fire = null;
	public Sprite water = null;
	public Sprite earth = null;
	public Sprite mud = null;
	public Sprite steam = null;
	public Sprite meteor = null;

	public Sprite getSprite(elementType type)
	{
		switch(type)
		{
			case elementType.fire:
				return fire;
			case elementType.water:
				return water;
			case elementType.earth:
				return earth;
			case elementType.steam:
				return steam;
			case elementType.mud:
				return mud;
			case elementType.meteor:
				return meteor;
			default:
				return null;
		}
				
	}
}

public class Player : MonoBehaviour {
	static float LANE_OFFSET = 2;
	static float SPELL_HEIGH_OFFSET = -2;
	Dictionary<Position, Lane> lanes;
	Lane currentLane = null;

	public GameObject spell;    // prefab for a spell
	public float spellSpeed = 10;
	public float attackSpeed = 0.3f;
	public float maxLife = 10;
	public float currentLife = 0;
	public ElementSprite elementGraph;
	SpriteRenderer playerSprite;
    Element castElement;
	PlayerDisplay display;

    bool canCastSpell = true;
	
	void Awake () {
		playerSprite = GetComponent<SpriteRenderer>();
		currentLife = maxLife;
		display = GetComponentInChildren<PlayerDisplay>();
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
			addElement(new Earth());
		}

		if (Input.GetMouseButtonDown(0) && canCastSpell)
		{
			castSpell();
		}
		else if (Input.GetMouseButtonDown(1))
		{
			resetSpell();
		}
	}

	void resetSpell()
	{
        castElement = null;
		display.updateElement(null,null);
	}

	void castSpell()
	{
		if (castElement == null)
		{
            Debug.LogFormat("<color=#2550F0>I cannot cast a spell without element.</color>");
			return;
		}
        Debug.Log("Launching " + castElement.GetType());
		GameObject go = (GameObject)Instantiate(spell, this.transform.position+ new Vector3(0, 0, SPELL_HEIGH_OFFSET), this.transform.rotation);
		go.GetComponent<Spell>().element = castElement;
		go.GetComponent<Rigidbody2D>().velocity = (currentLane.endLane.position - this.transform.position).normalized * spellSpeed;
		
		Sprite sprt = elementGraph.getSprite(castElement.type);
		if(sprt && go.GetComponent<SpriteRenderer>())
		go.GetComponent<SpriteRenderer>().sprite = sprt;

		StartCoroutine(waitCooldown());
	}

	IEnumerator waitCooldown()
	{
		resetSpell();
		canCastSpell = false;
		yield return new WaitForSeconds(attackSpeed);
		canCastSpell = true;
	}

	void addElement(Element elem)
	{
        if(castElement == null)
        {
            castElement = elem;
        } else
        {
            castElement = castElement.combine(elem);
        }
		if(castElement != null)
		display.updateElement(castElement, elementGraph.getSprite(castElement.type));
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
				playerSprite.flipX = true;
				switchLane(Position.Right);
			}
			else
			{
				playerSprite.flipX = false;
				switchLane(Position.Left);
			}
		}
		else if(isVertical)
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

    public void damage(float amount)
    {
		currentLife -= amount;
		display.updateLife(currentLife / maxLife);
		if (currentLife <= 0)
		{
			// LOST
			GetComponentInChildren<ReloadLevel>().showReloadLevel();
		}
		
    }
	
}
