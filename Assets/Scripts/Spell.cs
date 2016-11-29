using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

	public Element element = null;
	public float lifeTime = 5;

	void Start()
	{
		Destroy(this.gameObject, lifeTime);
	}
}
