using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionSystem : MonoBehaviour
{
    public PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && player.isNormal && player.direction != Vector2.zero)
        {
            Debug.Log("Enemigo detecta");
            EnemyTest localEnemy = collision.gameObject.GetComponent<EnemyTest>();
            localEnemy.isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemigo sale");
            EnemyTest localEnemy = collision.gameObject.GetComponent<EnemyTest>();
            localEnemy.isChasing = false;
        }
    }
}
