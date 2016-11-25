using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	Fire feu;

	// Use this for initialization
	void Start () {
		feu = new Fire();
		feu.combine(new Water());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
