using UnityEngine;
using System.Collections;
using System;

public class Fire : Element {

	public Fire(): base()
	{
		isStrong = new System.Type[] { typeof(Earth) };
	}

	public override Element combine(Element element)
	{
		return this;
	}

	public Element combine(Water element)
	{
		Debug.Log("steam");
		return new Steam();
	}

	public Element combine(Earth element)
	{
		Debug.Log("meteor");
		return new Meteor();
	}
}
