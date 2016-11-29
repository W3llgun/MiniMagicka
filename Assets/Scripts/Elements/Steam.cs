using UnityEngine;
using System.Collections;
using System;

public class Steam : Element {

	public Steam(): base()
	{
        isStrong = new elementType[] { elementType.meteor };
        type = elementType.steam;
	}
	
}
