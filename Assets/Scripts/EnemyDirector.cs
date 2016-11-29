using UnityEngine;
using System.Collections;

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

    private int currentEnemyStock = 0;
    private float currentRestockCooldown = 0f;

    private float currentLaunchCooldown = 0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        updateCooldowns();
	}

    private void updateCooldowns()
    {
        currentLaunchCooldown = Mathf.Max(currentLaunchCooldown - Time.deltaTime, 0f);
        currentRestockCooldown = Mathf.Max(currentRestockCooldown - Time.deltaTime, 0f);
        if(currentRestockCooldown <= 0f)
        {
            currentEnemyStock = Mathf.Min(currentEnemyStock + 1, param.maxEnemyStock);
        }
    }
   

    protected void createEnemy(Lane lane)
    {
        //TODO
        Debug.Log("Ah! You cannot defeat my" + "minion!");
        currentEnemyStock--;
    }
}
