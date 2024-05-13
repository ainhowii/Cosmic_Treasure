using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVPoint : MonoBehaviour
{
    public float fovAngle = 90f;
    public Transform fovPoint;
    public float range = 8;

    public float rotationSpeed = 45f;  // Velocidad de rotación 

    public Transform target;

    private void Start()
    {
        StartCoroutine(RotateAndWait());
    }

    private void Update()
    {

        Vector2 dir = Vector2.zero;
        float singleStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        float angle = Vector3.Angle(dir, fovPoint.up);
        RaycastHit2D r = Physics2D.Raycast(fovPoint.position, dir, range);

        if (angle < fovAngle / 2)  //Si el angulo es menor que 90 /2 ?????
        {
            if (r.collider.CompareTag("Player"))
            {
                // WE SPOTTED THE PLAYER
                Debug.Log("SEEN!");
                Debug.DrawRay(fovPoint.position, dir, Color.red);
            }
            else
            {
                Debug.Log("We dont seen");
            }
        }
    }

    IEnumerator RotateAndWait()   //Que sea continuo en loop
    {
        while (true) 
        {
            gameObject.transform.Rotate(0f, 0f, -45f);   // Rotar 45 grados a la derecha
            yield return new WaitForSeconds(3f); // Esperar 3 segundos
            gameObject.transform.Rotate(0f, 0f, 45f);    // Rotar 45 grados a la izquierda
            yield return new WaitForSeconds(3f);
        }
    }
}
