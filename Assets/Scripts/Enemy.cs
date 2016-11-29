using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyParam
{
    public float startingLife = 3.0f;
    public float minSpeed = 1f;
    public float maxSpeed = 1.75f;
}

public class Enemy : MonoBehaviour {

    EnemyParam param = new EnemyParam();
    public float life = 3f;
    public float damagesInflicted = 1f;
    public Element element;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void aimTarget(Player p)
    {
        this.GetComponent<Rigidbody2D>().velocity = (this.transform.position - p.transform.position).normalized
                                                        * Random.Range(param.minSpeed, param.maxSpeed);
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
