using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    float speed;
    Vector2 _direction;
    bool isReady;

    void Awake()
    {
        speed = 5f;
        isReady = false;
    }

    void Start()
    {
        
    }

    public void SetReady()
    {
        isReady = true;
    }

    void Update()
    {
        if(isReady)
        {
            Vector2 position = transform.position;

            position = new Vector2 (position.x, position.y - speed * Time.deltaTime);

            transform.position = position;

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));


            if(transform.position.y < min.y)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "PlayerShipTag"))
        {
            Destroy(gameObject);
        }
    }
}
