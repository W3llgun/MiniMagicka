using UnityEngine;
using System.Collections;
using System;

public class Mud : Element {

	public Mud(): base()
	{
		isStrong = new System.Type[] { typeof(Steam) };
	}
}
