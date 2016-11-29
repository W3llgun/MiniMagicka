using UnityEngine;
using System.Collections;
using System;

public class Earth : Element {

	public Earth(): base()
	{
		isStrong = new System.Type[] { typeof(Water) };
	}

	public override Element combine(Element element)
	{
		return this;
	}

	public Element combine(Fire element)
	{
		return new Meteor();
	}

	public Element combine(Water element)
	{
		return new Mud();
	}
}
