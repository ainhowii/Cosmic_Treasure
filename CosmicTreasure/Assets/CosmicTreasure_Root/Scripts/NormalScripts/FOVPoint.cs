using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FOVPoint : MonoBehaviour
{
    Animator animeitor;
    Light2D coneVision;

    public float fovAngle = 90f;
    public GameObject player;
    public float range = 80;
    

    public float rotationSpeed = .15f;  // Velocidad de rotación 

    public bool isSpotted; //Bool que indica si te ha detectado



    private void Start()
    {
        //StartCoroutine(RotateAndWait());
        isSpotted = false;
        animeitor = gameObject.GetComponent<Animator>();
        coneVision = gameObject.GetComponentInChildren<Light2D>();
        player = GameObject.FindGameObjectWithTag("Player");
       
    }

    private void Update()
    {
        Debug.Log("We dont seen");

        /* 
        if (isSpotted)
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = player.transform.position - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = rotationSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        

        

         Vector2 dir = Vector2.up;
         float angle = Vector3.Angle(dir, fovPoint.transform.up);
         RaycastHit2D r = Physics2D.Raycast(fovPoint.transform.position, dir, range);

         if (angle < fovAngle / 2)  //Si el angulo es menor que 90 /2 ?????
         {
             if (r.collider.CompareTag("Player"))
             {
                 // WE SPOTTED THE PLAYER
                 Debug.Log("SEEN!");
                 Debug.DrawRay(fovPoint.transform.position, dir, Color.green);
             }
             else
             {
                 Debug.Log("We dont seen");
                 Debug.DrawRay(fovPoint.transform.position, dir, Color.red);
             }
         }


 */

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("SEEN!");
            isSpotted = true;
            animeitor.enabled = false;
            coneVision.color = Color.red;
            //transform.rotation = Quaternion.LookRotation(transform.position,collision.transform.position);

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isSpotted = false;
            animeitor.enabled = true;
            coneVision.color = Color.white;
        }

       
    }

   
}
