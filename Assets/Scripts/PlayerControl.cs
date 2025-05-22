using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameManagerGO;

    public GameObject PlayerLaser;
    public GameObject LaserPosition01;
    public GameObject LaserPosition02;
    public GameObject Explosion;

    public AudioSource LaserSound;

    public TextMeshProUGUI LivesText;

    const int MaxLives = 3;
    int lives;

    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    public void Init()
    {
        lives = MaxLives;

        LivesText.text = lives.ToString ();

        transform.position = new Vector2(0, 0);

        gameObject.SetActive (true);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            LaserSound.Play();

            GameObject laser01 = (GameObject)Instantiate (PlayerLaser);
            laser01.transform.position = LaserPosition01.transform.position;

            GameObject laser02 = (GameObject)Instantiate (PlayerLaser);
            laser02.transform.position = LaserPosition02.transform.position;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.PauseScreen);
        }



        rb.linearVelocity = moveInput * moveSpeed;

        ClampToCameraView ();
    }

    void ClampToCameraView()
    {
        Vector3 pos = transform.position;
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        // Optional: account for sprite half-width/height if needed
        float halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        float halfHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        pos.x = Mathf.Clamp(pos.x, min.x + halfWidth, max.x - halfWidth);
        pos.y = Mathf.Clamp(pos.y, min.y + halfHeight, max.y - halfHeight);

        transform.position = pos;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "EnemyShipTag") || (col.tag == "EnemyLaserTag"))
        {
            PlayExplosion();
            lives--;
            LivesText.text = lives.ToString();
            if(lives == 0)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                gameObject.SetActive(false);
            }
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate (Explosion);

        explosion.transform.position = transform.position;
    }
}
