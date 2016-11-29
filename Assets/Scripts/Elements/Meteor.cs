using UnityEngine;
using System.Collections;
using System;

public class Meteor : Element {

    public Meteor() : base()
    {
        isStrong = new elementType[] { elementType.mud };
        type = elementType.meteor;
	}
}
