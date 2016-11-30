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

    void OnTriggerEnter2D(Collider2D c)
    {
        Enemy e = c.GetComponent<Enemy>();
        if(e)
        {
            e.damage(element, damagesInflicted);
        }
        this.gameObject.SetActive(false);
    }
}
