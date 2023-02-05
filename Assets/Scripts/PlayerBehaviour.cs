using UnityEngine;
using Piotr.SpawnWithPool;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(BoxCollider2D))]
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerBullet bulletPrefab;
    [SerializeField] private float yAxisMoveSpeed;

    private Rigidbody2D rigidBody;
    private Animator animator;

    private float moveInY;
    private float screenYBound;

    private Transform barrel;
  
    public float bulletDelay;
    private float nextfire;

    private bool playerActive;

    private SpawnWithPool bulletSpawner;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        barrel = transform.GetChild(0);

        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        screenYBound = screenBounds.y;
    }

    private void Start()
    {
        playerActive = true;

        PlayerStats.onGameOver += IsPlayerActive;

        bulletSpawner = new SpawnWithPool();
        bulletSpawner.toSpawn.Add(bulletPrefab.gameObject);
        bulletSpawner.Pool.Clear();
    }

    private void IsPlayerActive(bool isGameOver)
    {
        playerActive = !isGameOver;

        if (isGameOver)
        {
            rigidBody.velocity = Vector2.zero;
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
    }

    private void Update()
    {
        if (playerActive)
        {
            moveInY = Input.GetAxis("Vertical") * yAxisMoveSpeed; 
            rigidBody.velocity = new Vector2(0, moveInY);

            animator.SetFloat("Vertical", moveInY); 

            Vector3 movPos = transform.position; 
            movPos.y = Mathf.Clamp(transform.position.y, -screenYBound, screenYBound);
            transform.position = movPos;


            if (Input.GetButton("Fire1")) 
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (Time.time > nextfire)
        {
            nextfire = Time.time + bulletDelay;

            bulletSpawner.Pool.Get().
                GetComponent<Transform>().localPosition = barrel.position;
        }
    }
}
