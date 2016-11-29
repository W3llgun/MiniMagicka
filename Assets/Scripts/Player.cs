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

	// Use this for initialization
	void Awake () {
		lanes = new Dictionary<Position, Lane>();
		Lane[] childLanes = GetComponentsInChildren<Lane>();
		foreach (Lane l in childLanes)
		{
			lanes.Add(l.position, l);
		}
	}
	

	void Update () {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 10;// Vector3.Distance(this.transform.position, Camera.main.transform.position);
		updateLane(Camera.main.ScreenToWorldPoint(mousePos));
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
		else
		{
			//currentLane.ActivateLane(false);
		}

	}

	void switchLane(Position pos)
	{
		if (currentLane) currentLane.ActivateLane(false);
		currentLane = lanes[pos];
		currentLane.ActivateLane(true);
	}
}
