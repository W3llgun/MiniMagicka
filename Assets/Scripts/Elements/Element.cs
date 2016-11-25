using UnityEngine;
using System.Collections;

public enum Type
{
	Feu,
	Eau,
	Terre
}

public abstract class Element : MonoBehaviour{

	public Type type;
	public float damage = 10;
	public Element[] isWeak = null;
	public Element[] isStrong = null;

	public bool isWeakAgainst(Type element)
	{
		foreach (var item in isWeak)
		{
			if (item.type == element)
				return true;
		}
		return false;
	}

	public bool isStrongAgainst(Type element)
	{
		foreach (var item in isStrong)
		{
			if (item.type == element)
				return true;
		}
		return false;
	}

	public float dealDamage()
	{
		return damage;
	}

	public abstract void combine(Element element);
}
