using UnityEngine;
using System.Collections;

public enum elementType
{
    none,
    fire,
    water,
    earth,
    steam,
    meteor,
    mud
}


public abstract class Element {
	
	public float damage = 10;
	public elementType[] isStrong = null;

    public elementType type;

	public Element()
	{

	}

	bool isStrongAgainst(Element element)
	{
		if(isStrong != null)
		{
			for(int i = 0; i < isStrong.Length; ++i)
            {
                if (isStrong[i] == element.type) return true;
            }
		}
		return false;
	}

	public float dealDamage(Element element)
	{
		if(isStrongAgainst(element))
		{
			return damage;
		}
		return 0;
	}

	public virtual Element combine(Element element)
	{
		return this;
	}
}
