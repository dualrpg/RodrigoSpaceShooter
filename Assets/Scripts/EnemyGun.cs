using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyLaser;
    void Start()
    {
        InvokeRepeating("FireEnemyLaser", 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireEnemyLaser()
    {
        GameObject laser = (GameObject)Instantiate (EnemyLaser);
        laser.transform.position = transform.position;

        laser.GetComponent<EnemyLaser>().SetReady();
    }
}
