using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyFollow : MonoBehaviour
{
    //public AIPath aIPath;

    Vector2 direction;

    AIPath path;

    [Header ("Enemy Attributes")]
    public float speed;
    public float lineOfSite;  //TE PERSIGUE MIENTRAS ESTÉS EN ESTE RANGO
    public float shootingRange;  //EN ESTE RANGO TE DEJA DE PERSEGUIR Y TE DISPARA
    public float fireRate = 1f;  //PARA QUE NO TE TIRE UNA RAFAGA DE TIROS
    private bool playerPositionSaved;  //Guardar posición del jugador
    private Vector2 playerPosOnShoot;
    private float atkCD;  //CoolDown
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
        //faceVelocity();

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange)
        {
            if (atkCD <= 0)
            {
                atkCD = fireRate;
                GameObject newBullet = Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
                Vector2 difference = newBullet.transform.position - player.position;
                //difference.normalized;

            }
        }
       
    }

    private void FixedUpdate()
    {
        if (atkCD > 0)
        {
            atkCD -= Time.deltaTime;

        }
        
    }

    void faceVelocity()
    {
        //direction = aIPath.desiredVelocity;

        transform.right = direction;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
