using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    NavMeshAgent agent;
    public enum EnemyState { patroling, chasing, attacking}
    Vector2 dir;

    [Header("Enemy Attributes")]
    public float fireRate = 1f;  //PARA QUE NO TE TIRE UNA RAFAGA DE TIROS
    private float atkCD;  //CoolDown
    public GameObject bullet;
    [SerializeField] GameObject bulletParent;
    [SerializeField] float attackDistance;

    [Header("Rotation Fov")]
    public float speedRotation;
    public float rotationModifier;

    [Header("Fov Point")]
    public float fovAngle = 90f;
    public Transform fovPoint;
    public float range = 8;

    [Header("Patrol")]
    [SerializeField] private float speedMovement;
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private float minimumDistance;
    private int randomNumber;
    private SpriteRenderer spriteRenderer;

    [Header("Random Patrol")]
    public float radius;
    public Transform centrePoint;

    [Header("States Enemy")]
    [SerializeField] EnemyState currentState;
    private bool isChasing;
    private bool isShooting;

    public GameObject player;

    public Transform playerD;

    public Transform target;

    private void Start()
    {
        currentState = EnemyState.patroling;

        //PATHFINDING
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //PATROL
        randomNumber = Random.Range(0, movementPoints.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
        if (!isChasing && !isShooting) { currentState = EnemyState.patroling; }
        if (isChasing && !isShooting) { currentState = EnemyState.chasing; }
        if (isChasing && isShooting) { currentState = EnemyState.attacking; }

        EnemyStateManagement();

        //transform.position = Vector2.MoveTowards(transform.position, movementPoints[randomNumber].position, speedMovement * Time.deltaTime);  //SE MUEVE RANDOM A LOS PUNTOS


        float distanceFromPlayer = Vector2.Distance(playerD.position, transform.position);
        dir = target.position - transform.position;
        float angle = Vector3.Angle(dir, fovPoint.up);
        RaycastHit2D r = Physics2D.Raycast(fovPoint.position, dir, range);

        /*
        if (Vector2.Distance(transform.position, movementPoints[randomNumber].position) < minimumDistance)  //Patrol Estandar
        {
            randomNumber = Random.Range(0, movementPoints.Length);
            Rotate();
        }
        */

        if (distanceFromPlayer < 5)
        {
            Debug.Log("Estoy cerca...");
        }

        if (angle < fovAngle / 2)
        {
            if (r.collider.CompareTag("Player"))
            {
                if (distanceFromPlayer > attackDistance)
                {
                    isChasing = true;
                    Debug.Log("Detecto al player");
                }
                else
                {
                    isShooting = true;
                    Debug.Log("Estoy atacando");
                }
                
            }
            else
            {
                isChasing = false;
                isShooting = false;
                Debug.Log("We dont seen");
            }
        }
        
    }

    void EnemyStateManagement()
    {
        switch (currentState)
        {
            case EnemyState.patroling:
                Patrol();
                    break;
                
            case EnemyState.chasing:
                ChasePlayer();
                break;
                
            case EnemyState.attacking:
                EnemyAttack();
                break;
                
        }
            
    }

    private void Patrol()
    {
        agent.SetDestination(transform.position);
        transform.position = Vector2.MoveTowards(transform.position, movementPoints[randomNumber].position, speedMovement * Time.deltaTime);  //SE MUEVE RANDOM A LOS PUNTOS
        LookAt(movementPoints[randomNumber].transform);

        if (Vector2.Distance(transform.position, movementPoints[randomNumber].position) < minimumDistance)  //Patrol Estandar
        {
            randomNumber = Random.Range(0, movementPoints.Length);
            
        }
    }

    void ChasePlayer()
    {
        
        LookAt(player.transform);
        agent.SetDestination(target.position);
        Debug.Log("SEEN PLAYER!");
        Debug.DrawRay(fovPoint.position, dir, Color.red);
    }

    void EnemyAttack()
    {
        agent.SetDestination(transform.position);
        Debug.Log("ENTRA TIRO");
        LookAt(player.transform);
        atkCD = fireRate;
        GameObject newBullet = Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
    }

    private void Rotate()
    {
        if (transform.position.x < movementPoints[randomNumber].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void LookAt(Transform target)
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float anglee = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(anglee, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speedRotation);
    }

    private void FixedUpdate()
    {
        if (atkCD > 0)
        {
            atkCD -= Time.deltaTime;

        }

    }
}
