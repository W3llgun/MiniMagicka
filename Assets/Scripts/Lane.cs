using UnityEngine;
using System.Collections;

public class Lane : MonoBehaviour {

	public Position position;
	public Transform endLane;
	public GameObject indicator;

	private void Awake()
	{
		if (indicator) indicator.SetActive(false);
	}

	public void ActivateLane(bool value)
	{
		if (indicator)
		{
			if (value)
			{
				indicator.SetActive(true);
			}
			else
			{
				indicator.SetActive(false);
			}
		}
		
	}
}
