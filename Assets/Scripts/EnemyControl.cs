using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    GameObject scoreText;
    float speed;
    public GameObject Explosion;
    private bool isDestroyed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 1f;

        scoreText = GameObject.FindGameObjectWithTag ("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2 (position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));

        if(transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isDestroyed) return; 
        if((col.tag == "PlayerShipTag") || (col.tag == "PlayerLaserTag"))
        {
            isDestroyed = true; 
            PlayExplosion();
            scoreText.GetComponent<GameScore>().Score += 100;
            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate (Explosion);

        explosion.transform.position = transform.position;
    }

}
