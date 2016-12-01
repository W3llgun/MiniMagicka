using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public GameObject panelControl;
	float escapeTime = 1.5f;
	float currentEscape = 0;
	// Use this for initialization
	void Start () {
		if(!panelControl) panelControl = transform.Find("Parchemin").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Tab))
		{
			if (!panelControl.activeSelf)
			{
				Time.timeScale = 0;
				panelControl.SetActive(true);
			}
			return;
		}
		else
		{
			
			if (panelControl.activeSelf)
			{
				Time.timeScale = 1;
				panelControl.SetActive(false);
			}
			
		}

		if (Input.GetKey(KeyCode.Escape))
		{
			
			currentEscape += Time.deltaTime;
			if(currentEscape>escapeTime)
			{
				//EXIT
				Application.Quit();
			}
		}
		else
		{
			currentEscape = 0;
		}
	}
}
