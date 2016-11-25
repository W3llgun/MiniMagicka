using UnityEngine;
using System.Collections;
using System;

public class Water : Element {

	public Water(): base()
	{
		isStrong = new System.Type[] { typeof(Fire) };
	}
	

	public Element combine(Fire element)
	{
		return new Steam();
	}
	
	public Element combine(Earth element)
	{
		return new Mud();
	}
}
