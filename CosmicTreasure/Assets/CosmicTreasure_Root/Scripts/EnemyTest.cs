using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    //CODEAR: ARREGLAR EL RANDOM PATROL, QUE DISPARE BIEN, QUE ALERTE A SUS COMPAÑEROS

    public NavMeshAgent agent;
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

        float distanceFromPlayer = Vector2.Distance(playerD.position, transform.position);  //Distancia con el Player

        //RAYCAST
        dir = target.position - transform.position;
        float angle = Vector3.Angle(dir, fovPoint.up);
        RaycastHit2D r = Physics2D.Raycast(fovPoint.position, dir, range);


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

                // Todos los enemigos con el tag "enemy" && que hagan trigger con el área de la habitación, pasan a modo chasing (isChasing = true)
                // Si el raycast del enemigo ya no detecta al player (on trigger exit) todos pasan a Random Patrol
                
            }
            else
            {
                isChasing = false;
                isShooting = false;
                Debug.Log("We dont seen");
            }

            
        }
        
    }

    private void OnTriggerExit(Collider other)  //Cuando el raycast deja de colisionar con el player
    {
        if (other.CompareTag("Player"))  //Empieza el Patrol Random
        {
            if (agent.remainingDistance <= agent.stoppingDistance) //done with path
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, radius, out point)) //pass in our centre point and radius of area
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                    agent.SetDestination(point);
                }
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

    bool RandomPoint(Vector3 center, float radius, out Vector3 result)            //RANDOM PATROL
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * radius; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void FixedUpdate()
    {
        if (atkCD > 0)
        {
            atkCD -= Time.deltaTime;

        }

    }
}
