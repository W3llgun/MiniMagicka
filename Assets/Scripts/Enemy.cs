using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float life = 3f;
    public float damagesInflicted = 1f;
    public Element element;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void goTo(Player p)
    {

    }

    void OnCollisionEnter(Collision c)
    {
        Player p = c.collider.GetComponent<Player>();
        if(p != null)
        {
            p.damage(damagesInflicted);            
        }
    }

    /// <summary>
    /// Damages the monster by a certain amount.
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="e"></param>
    public void damage(Element e, float amount)
    {
        amount = e.getDamageAgainst(this.element, amount);
        life = Mathf.Max(life - amount, 0f);
        if(life <= 0f)
        {
            die();
        }
    }

    public void die()
    {
        Destroy(this.gameObject);
    }

}
