using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

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

public class EnemyDirector : MonoBehaviour {

    public EnemyDirectorParameters param = new EnemyDirectorParameters();

    public List<Transform> spawnPoints = new List<Transform>();

    public GameObject enemyPrefab;

    public Player player;

    private int currentEnemyStock = 0;
    private float currentRestockCooldown = 0f;

    private float currentLaunchCooldown = 0f;


    void Awake()
    {
        Debug.Assert(spawnPoints.Count > 0, "No enemy spawn point!");
 
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        updateCooldowns();
        if(canLaunchEnemy())
        {
            createEnemy(chooseEnemyElement());
        }
	}

    private Element chooseEnemyElement()
    {
        Element chosenElement;
        Array values = Enum.GetValues(typeof(elementType));
        elementType i = (elementType)values.GetValue(UnityEngine.Random.Range(1, values.Length));
        switch(i)
        {
            case elementType.earth: chosenElement = new Earth(); break;
            case elementType.fire: chosenElement = new Fire(); break;
            case elementType.meteor: chosenElement = new Meteor(); break;
            case elementType.mud: chosenElement = new Mud(); break;
            case elementType.steam: chosenElement = new Steam(); break;
            case elementType.water: chosenElement = new Water(); break;
            default: throw new System.Exception();
        }
        return chosenElement;
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

    protected void createEnemy(Element e)
    {
        int spawnPosition = UnityEngine.Random.Range(0, spawnPoints.Count);

        GameObject created = Instantiate(enemyPrefab, spawnPoints[spawnPosition].position, spawnPoints[spawnPosition].rotation) as GameObject;

        if (created != null)
        {
            Enemy spawnedEnemy = created.GetComponent<Enemy>();
            Debug.Assert(spawnedEnemy != null, "enemy prefab does not contain enemy script!");
            spawnedEnemy.element = e;
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
        
    }
}
