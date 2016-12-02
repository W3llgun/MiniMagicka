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

    const float VS_WEAK_DAMAGE_MULTIPLIER = 4.0f;

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

    /// <summary>
    /// Returns the damage received, if this element is the one dealing the damages.
    /// </summary>
    /// <param name="element">element that will receives the damage.</param>
    /// <returns>The new damage value.</returns>
	public float getDamageAgainst(Element element, float amount)
	{
		if(isStrongAgainst(element))
		{
            //If we are strong, deals tripled damages.
			return amount * VS_WEAK_DAMAGE_MULTIPLIER;
		} else if (element.isStrongAgainst(this))
        {
            //If we're weak, we does no damage.
            return 0f;
        }
		return amount;
	}

	public virtual Element combine(Element element)
	{
		return this;
	}
}
