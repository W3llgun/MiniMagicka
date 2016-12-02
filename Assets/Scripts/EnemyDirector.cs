using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyDirectorParameters
{
    [Tooltip("The delay before each enemySpawns.")]
    public float enemyLaunchCooldown = 0.5f;
    [Tooltip("Time to have a new enemy in stock to send.")]
    public float enemyStockRechargeDelay = 2.75f;
    [Tooltip("The maximum number of ennemies that can be sent at a time.")]
    public int maxEnemyStock = 5;
}

public class spawnChances
{
    public Enemy e;
    public int chances = 4;

    public spawnChances(Enemy e, int chance)
    {
        this.e = e; this.chances = chance;
    }
}

public class EnemyDirector : MonoBehaviour {

    public EnemyDirectorParameters param = new EnemyDirectorParameters();

    public List<Transform> spawnPoints = new List<Transform>();

    public List<GameObject> enemyPrefabs;

    public Player player;

    private List<spawnChances> enemySpawnChances;

    private int currentEnemyStock = 0;
    private float currentRestockCooldown = 0f;

    private float currentLaunchCooldown = 0f;


    void Awake()
    {
        Debug.Assert(spawnPoints.Count > 0, "No enemy spawn point!");
        enemySpawnChances = new List<spawnChances>(enemyPrefabs.Count);
        for(int i = 0; i < enemyPrefabs.Count; ++i)
        {
            enemySpawnChances.Add(new spawnChances(enemyPrefabs[i].GetComponent<Enemy>(), 4));
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        updateCooldowns();
        if(canLaunchEnemy())
        {
            createEnemy(chooseEnemy());
        }
	}

    private int chooseEnemy()
    {
        int cumulativeChances = 0;
        int chosenRandom = 0;
        int maxRandom = 0;
        for(int i = 0; i < enemySpawnChances.Count; ++i)
        {
            maxRandom += enemySpawnChances[i].chances;
        }
        chosenRandom = Random.Range(0, maxRandom);
        for(int i = 0; i < enemySpawnChances.Count; ++i)
        {
            cumulativeChances += enemySpawnChances[i].chances;
            if(cumulativeChances >= chosenRandom)
            {
                return i;
            }
        }
        return enemyPrefabs.Count-1;
    }

    private void updateCooldowns()
    {
        currentLaunchCooldown = Mathf.Max(currentLaunchCooldown - Time.deltaTime, 0f);
        currentRestockCooldown = Mathf.Max(currentRestockCooldown - Time.deltaTime, 0f);
        if(currentRestockCooldown <= 0f)
        {
            currentEnemyStock = Mathf.Min(currentEnemyStock + 1, param.maxEnemyStock);
            currentRestockCooldown = param.enemyStockRechargeDelay;
        }
    }
   
    private bool canLaunchEnemy()
    {
        return currentLaunchCooldown <= 0f && currentEnemyStock >= 1;
    }

    protected void createEnemy(int enemyNumber)
    {
        int spawnPosition = Random.Range(0, spawnPoints.Count);

        GameObject created = Instantiate(enemyPrefabs[enemyNumber], spawnPoints[spawnPosition].position, spawnPoints[spawnPosition].rotation) as GameObject;

        if (created != null)
        {
            Enemy spawnedEnemy = created.GetComponent<Enemy>();
            Debug.Assert(spawnedEnemy != null, "enemy prefab does not contain enemy script!");
            spawnedEnemy.init(enemyNumber);
            spawnedEnemy.linkedDirector = this;
            spawnedEnemy.aimTarget(player);
    
            Debug.Log("Ah! You cannot defeat my " + spawnedEnemy.element.type +" minion!");
            currentEnemyStock--;
            currentLaunchCooldown = param.enemyLaunchCooldown;
        } else
        {
            Debug.LogError("Couldn't create enemy.");
        }
    }

    public void informDeath(Enemy e)
    {
        Debug.LogFormat("<b>THIS. IS. IMPOSSIBLE!</b>");
        reduceSpawnChances(e.enemyTypeNumber);
    }

    public void reduceSpawnChances(int i)
    {
        Debug.Assert(i < enemySpawnChances.Count && i >= 0);
        if (enemySpawnChances[i].chances > 1)
        {
            enemySpawnChances[i].chances--;
            if (i < enemySpawnChances.Count-1)
            {
                enemySpawnChances[i+1].chances++;
            } else if (i == enemySpawnChances.Count-1)
            {
                enemySpawnChances[0].chances++;
            }
        }
    }
}
