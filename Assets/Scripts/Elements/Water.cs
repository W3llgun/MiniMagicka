using UnityEngine;
using System.Collections;
using System;

public class Water : Element {

	public Water(): base()
	{
		isStrong = new elementType[] {elementType.fire};
        type = elementType.water;
	}

	public override Element combine(Element element)
	{
        switch (element.type)
        {
            case elementType.earth: return new Mud();
            case elementType.fire: return new Steam();
            default: return this;
        }
    }

}
