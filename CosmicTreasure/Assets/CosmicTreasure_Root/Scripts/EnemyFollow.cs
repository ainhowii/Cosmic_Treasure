using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public float lineOfSite;  //TE PERSIGUE MIENTRAS ESTÉS EN ESTE RANGO
    public float shootingRange;  //EN ESTE RANGO TE DEJA DE PERSEGUIR Y TE DISPARA
    public float fireRate = 1f;  //PARA QUE NO TE TIRE UNA RAFAGA DE TIROS
    private bool playerPositionSaved;  //Guardar posición del jugador
    private Vector2 playerPosOnShoot;
    private float nextFireTime;
    public GameObject bullet;
    [SerializeField] GameObject bulletParent;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPositionSaved = false;


    }

    private void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime <Time.time)
        {
            if (playerPositionSaved == false)
            {
                Debug.Log("Entra a if");
                playerPosOnShoot = player.position;
                Debug.Log("PlayerPosOnShoot" + playerPosOnShoot);
                playerPositionSaved = true;

                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);

                Invoke("BoolReset", 1);
            }
        }
       
    }

    private void BoolReset()
    {
        playerPositionSaved = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
