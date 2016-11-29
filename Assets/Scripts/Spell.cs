using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

	public Element element = null;
	public float lifeTime = 5;
    public float damagesInflicted = 1f;

	void Start()
	{
		Destroy(this.gameObject, lifeTime);
	}
}
