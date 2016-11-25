using UnityEngine;
using System.Collections;


public abstract class Element {
	
	public float damage = 10;
	public System.Type[] isStrong = null;

	public Element()
	{

	}

	bool isStrongAgainst(Element element)
	{
		if(isStrong != null)
		{
			System.Type type = element.GetType();
			foreach (var item in isStrong)
			{
				if (item == type)
					return true;
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
