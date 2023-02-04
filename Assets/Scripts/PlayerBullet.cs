using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerBullet : MonoBehaviour, IReturningToPool
{
    public Action<GameObject> thisBulletDestroyed;
    private Rigidbody2D rigidBody;
    [SerializeField] private float bulletSpeed;

    public void OnInitReturningToPool(Action<GameObject> returnToPoolAction)
    {
        thisBulletDestroyed = returnToPoolAction;
    }


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody.MovePosition(rigidBody.position + bulletSpeed * Vector2.right * Time.fixedDeltaTime);
    }

    void OnBecameInvisible()
    {
        thisBulletDestroyed.Invoke(gameObject);
    }

    private void OnCollisionEnter2D()
    {
        thisBulletDestroyed.Invoke(gameObject);
    }
}