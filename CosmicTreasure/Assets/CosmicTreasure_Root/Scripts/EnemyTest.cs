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
    private bool isFollowing;
    private bool isShooting;

    public GameObject player;

    public Transform playerD;

    public Transform target;

    private void Start()
    {
        //PATHFINDING
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        //PATROL
        randomNumber = Random.Range(0, movementPoints.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Rotate();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movementPoints[randomNumber].position, speedMovement * Time.deltaTime);

        float distanceFromPlayer = Vector2.Distance(playerD.position, transform.position);
        Vector2 dir = target.position - transform.position;
        float angle = Vector3.Angle(dir, fovPoint.up);
        RaycastHit2D r = Physics2D.Raycast(fovPoint.position, dir, range);

        if (Vector2.Distance(transform.position, movementPoints[randomNumber].position) < minimumDistance)  //Patrol Estandar
        {
            randomNumber = Random.Range(0, movementPoints.Length);
            Rotate();
        }

        if (angle < fovAngle / 2)
        {
            if (r.collider.CompareTag("Player"))
            {
                isFollowing = true;
                LookAt();
                agent.SetDestination(target.position);
                Debug.Log("SEEN PLAYER!");
                Debug.DrawRay(fovPoint.position, dir, Color.red);
            }
            else if (r.collider.CompareTag("Player") && distanceFromPlayer < range - 4)         //FALTA CORREGIR
            {
                Debug.Log("ENTRA TIRO");
                isShooting = true;
                LookAt();
                atkCD = fireRate;
                GameObject newBullet = Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
                
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, movementPoints[randomNumber].position, speedMovement * Time.deltaTime);
                Debug.Log("We dont seen");
            }
        }
        if (isFollowing == false)
        {
            isFollowing = false;
            Searching();
        }
    }

    private void Searching()  //ESTADO DE BUSQUEDA
    {
        isFollowing = false;
        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {

            Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
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

    private void LookAt()
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;
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
