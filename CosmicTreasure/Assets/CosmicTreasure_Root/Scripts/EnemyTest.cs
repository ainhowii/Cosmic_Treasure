using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    NavMeshAgent agent;

    [Header("Enemy Attributes")]
    public float fireRate = 1f;  //PARA QUE NO TE TIRE UNA RAFAGA DE TIROS
    private float atkCD;  //CoolDown
    public GameObject bullet;
    [SerializeField] GameObject bulletParent;

    [Header("Rotation Fov")]
    public float speedRotation;
    public float rotationModifier;

    [Header("Fov Point")]
    public float fovAngle = 90f;
    public Transform fovPoint;
    public float range = 8;

    [Header("States Enemy")]
    private bool isFollowing;
    private bool isShooting;

    public GameObject player;

    public Transform playerD;

    public Transform target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        float distanceFromPlayer = Vector2.Distance(playerD.position, transform.position);
        Vector2 dir = target.position - transform.position;
        float angle = Vector3.Angle(dir, fovPoint.up);
        RaycastHit2D r = Physics2D.Raycast(fovPoint.position, dir, range);

       // agent.SetDestination(target.position);

        if (angle < fovAngle / 2)
        {
            if (r.collider.CompareTag("Player"))
            {
                isFollowing = true;
                Vector3 vectorToTarget = player.transform.position - transform.position;
                float anglee = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
                Quaternion q = Quaternion.AngleAxis(anglee, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speedRotation);
                agent.SetDestination(target.position);
                Debug.Log("SEEN PLAYER!");
                Debug.DrawRay(fovPoint.position, dir, Color.red);
            }
            else if (r.collider.CompareTag("Player") && distanceFromPlayer < range - 4)         //FALTA CORREGIR
            {
                Debug.Log("ENTRA TIRO");
                isShooting = true;
                Vector3 vectorToTarget = player.transform.position - transform.position;
                float anglee = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
                Quaternion q = Quaternion.AngleAxis(anglee, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speedRotation);
                atkCD = fireRate;
                GameObject newBullet = Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
                
            }
            else
            {
                Debug.Log("We dont seen");
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
}
