using UnityEngine;
using System.Collections;
using System;

public class Earth : Element {

	public Earth(): base()
	{
        isStrong = new elementType[] { elementType.water };
        type = elementType.earth;
	}

	public override Element combine(Element element)
	{
        switch(element.type)
        {
            case elementType.water: return new Mud();
            case elementType.fire: return new Meteor();
            default: return this;
        }
	}

}
