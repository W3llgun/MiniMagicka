using UnityEngine;
using System.Collections;
using System;

public class Fire : Element {

	public Fire(): base()
	{
		isStrong = new System.Type[] { typeof(Earth) };
	}
	
	public Element combine(Water element)
	{
		return new Steam();
	}

	public Element combine(Earth element)
	{
		return new Meteor();
	}
}
