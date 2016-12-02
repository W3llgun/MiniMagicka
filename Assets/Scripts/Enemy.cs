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
    public elementType type;
    private EnemyDirector master;

    public int enemyTypeNumber = 0;

    public EnemyDirector linkedDirector
    {
        private get { return master; }
        set { master = value; }
    }

    void Awake()
    {

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void init(int enemyNumber)
    {
        enemyTypeNumber = enemyNumber;
        switch (type)
        {
            case elementType.earth: element = new Earth(); break;
            case elementType.fire: element = new Fire(); break;
            case elementType.meteor: element = new Meteor(); break;
            case elementType.mud: element = new Mud(); break;
            case elementType.steam: element = new Steam(); break;
            case elementType.water: element = new Water(); break;
            default: Debug.LogWarning("No element associated to enemy"); break;
        }
    }

    public void aimTarget(Player p)
    {
        this.GetComponent<Rigidbody2D>().velocity = (p.transform.position - this.transform.position).normalized
                                                        * Random.Range(param.minSpeed, param.maxSpeed);
    }


    void OnTriggerEnter2D(Collider2D c)
    {
        Player p = c.GetComponent<Player>();
        if(p != null)
        {
            p.damage(damagesInflicted);
            die(true);
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
            die(false);
        }
    }

    public void die(bool selfDestroy)
    {
        Destroy(this.gameObject);
        if (!selfDestroy)
        {
            master.informDeath(this);
        }
    }

}
