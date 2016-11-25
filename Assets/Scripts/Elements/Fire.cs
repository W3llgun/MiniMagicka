using UnityEngine;
using System.Collections;
using System;

public class Fire : Element {

	public override void combine(Element element)
	{
		Debug.Log("Elem");
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
