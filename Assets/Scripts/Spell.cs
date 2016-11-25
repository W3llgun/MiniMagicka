using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

	Element element = null;

	public void setElement(Element elem)
	{
		this.element = elem;
	}

	public void OnCollisionEnter(Collision collision)
	{
		
	}

}
