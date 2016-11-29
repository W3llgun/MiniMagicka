using UnityEngine;
using System.Collections;
using System;

public class Fire : Element {

	public Fire(): base()
	{
        isStrong = new elementType[] { elementType.earth };
        type = elementType.fire;
	}

	public override Element combine(Element element)
	{
        switch (element.type)
        {
            case elementType.earth: return new Meteor();
            case elementType.water: return new Steam();
            default: return this;
        }
    }
}
