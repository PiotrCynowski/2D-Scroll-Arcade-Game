using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyBehaviour : MonoBehaviour, IReturningToPool
{
    private Action<GameObject> thisEnemyKilled;

    [SerializeField] private int enemyHP; 
    [SerializeField] private int enemySpeed;

    private int currentEnemyHP;
    private bool isReadyToUse;

    public void OnInitReturningToPool(Action<GameObject> objectForPoolRelease)
    {
        thisEnemyKilled = objectForPoolRelease;
        currentEnemyHP = enemyHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullets"))
        {
            TakeDamage();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && PlayerStats.Instance != null)
        {
            PlayerStats.Instance.PlayerLifes--;
        }
    }

    void OnBecameInvisible()
    {
        if (!isReadyToUse)
        {
            isReadyToUse = true;
            thisEnemyKilled.Invoke(gameObject);
        }
    }

    void OnDisable()
    {
        currentEnemyHP = 3;
    }

    private void TakeDamage()
    {
        currentEnemyHP--;
        if (currentEnemyHP <= 0)
        {
            if (PlayerStats.Instance != null)
            {
                PlayerStats.Instance.PlayerScore++;
            }

            isReadyToUse = true;
            thisEnemyKilled.Invoke(gameObject);
        }     
    }
}
