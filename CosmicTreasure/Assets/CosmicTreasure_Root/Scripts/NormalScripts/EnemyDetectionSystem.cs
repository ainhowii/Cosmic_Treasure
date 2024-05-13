using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionSystem : MonoBehaviour
{
    public PlayerController player;

    public Vector3 lastPosition;

    private bool heared;

    private void Update()
    {
        if (player.direction != Vector2.zero)
        {
            lastPosition = transform.position;
        }
         
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && player.isNormal && player.direction != Vector2.zero)
        {
            Debug.Log("Enemigo detecta");
            heared = true;
            EnemyTest localEnemy = collision.gameObject.GetComponent<EnemyTest>();
            localEnemy.isPatroling = false;
            localEnemy.ChasePlayer(lastPosition);
            localEnemy.agent.SetDestination(lastPosition);
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
    */
}
