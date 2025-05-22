using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject SmallEnemy;
    float maxSpawnRateInSeconds = 5f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2(0,0));
        Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2(1,1));
        GameObject anEnemy = (GameObject)Instantiate (SmallEnemy);
        anEnemy.transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);

        SchelduleNextEnemySpawn ();
    }

    void SchelduleNextEnemySpawn()
    {
        float spawnInSeconds;

        if(maxSpawnRateInSeconds > 1f)
        {
            spawnInSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
            spawnInSeconds = 1f;
        Invoke ("SpawnEnemy", spawnInSeconds);
    }

    //Aumentar dificultad
    void IncreaseSpawnRate()
    {
        if(maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;

        if(maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }
    public void ScheduleEnemySpawner()
    {
        maxSpawnRateInSeconds = 5f;

        Invoke("SpawnEnemy", maxSpawnRateInSeconds);

        //Aumentamos cada 30 segundos
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    public void UnscheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
