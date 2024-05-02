using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] Transform target;

    // CODEAR: QUE EL PLAYER TE SIGA Y ATACA SOLO SI ESTÁS EN SU FOV. Que el enemigo mire al player al perseguirle y atacarle

    [Header("Enemy Attributes")]
    public float lineOfSite;  //TE PERSIGUE MIENTRAS ESTÉS EN ESTE RANGO
    public float shootingRange;  //EN ESTE RANGO TE DEJA DE PERSEGUIR Y TE DISPARA
    public float fireRate = 1f;  //PARA QUE NO TE TIRE UNA RAFAGA DE TIROS
    private float atkCD;  //CoolDown
    public GameObject bullet;
    [SerializeField] GameObject bulletParent;
    public Transform player;

    [Header("Fov Point")]
    public float fovAngle = 90f;
    public Transform fovPoint;
    public float range = 8;

    [Header("Rotation FOV")]
    public float speedRotation;
    public float rotationModifier;

    [Header("States Enemy")]
    private bool isFollowing;
    private bool isShooting;

    private NavMeshAgent navSpeed;   //ACCEDER AL SPEED DEL NAVMESHAGENT
    NavMeshAgent agent;     

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        Vector2 dir = target.position - transform.position;
        float angle = Vector3.Angle(dir, fovPoint.up);
        RaycastHit2D r = Physics2D.Raycast(fovPoint.position, dir, range);

        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange && player != null)   //Estado de PERSEGUIR
        {
            isFollowing = true;
            Vector3 vectorToTarget = player.transform.position - transform.position;
            float anglee = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speedRotation);
            agent.SetDestination(target.position);
            //transform.position = Vector2.MoveTowards(this.transform.position, player.position, navSpeed.speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && angle < fovAngle / 2)    //Estado de apuntar y DISPARAR
        {
            if (atkCD <= 0 && r.collider.CompareTag("Player"))
            {
                Debug.Log("SEEN Player!");
                Debug.DrawRay(fovPoint.position, dir, Color.red);
                isShooting = true;
                Vector3 vectorToTarget = player.transform.position - transform.position;
                float anglee = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speedRotation);
                atkCD = fireRate;
                GameObject newBullet = Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();


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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
