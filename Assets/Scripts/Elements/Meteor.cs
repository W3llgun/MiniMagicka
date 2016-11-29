using UnityEngine;
using System.Collections;
using System;

public class Meteor : Element {
	
	public Meteor(): base()
	{
		isStrong = new System.Type[] {typeof(Mud)};
	}
}
