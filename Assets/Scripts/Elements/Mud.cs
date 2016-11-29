using UnityEngine;
using System.Collections;
using System;

public class Mud : Element {

	public Mud(): base()
	{
		isStrong = new elementType[] {elementType.steam};
        type = elementType.mud;
	}
}
