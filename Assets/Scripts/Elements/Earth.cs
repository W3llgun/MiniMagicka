using UnityEngine;
using System.Collections;
using System;

public class Earth : Element {

	public override void combine(Element element)
	{
		
	}

	public void combine(Fire element)
	{
		Debug.Log("Feu");
	}

	public void combine(Water element)
	{
		Debug.Log("Feu");
	}

	public void combine(Earth element)
	{
		Debug.Log("Feu");
	}
}
