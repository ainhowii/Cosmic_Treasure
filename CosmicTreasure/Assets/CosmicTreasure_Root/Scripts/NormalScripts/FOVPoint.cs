using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVPoint : MonoBehaviour
{
    public float fovAngle = 90f;
    public GameObject fovPoint;
    public float range = 8;

    public float rotationSpeed = .15f;  // Velocidad de rotación 

   



    private void Start()
    {
        //StartCoroutine(RotateAndWait());
    }

    private void Update()
    {

        
        transform.Rotate(0, 0, rotationSpeed);
        /*if (transform.rotation.z <= 0)
        {
            transform.Rotate(0, 0, rotationSpeed);
        }
        else if (transform.rotation.z > 90)
        {
            transform.Rotate(0, 0, -rotationSpeed);
        }*/
        Vector2 dir = Vector2.zero;
        float angle = Vector3.Angle(dir, fovPoint.transform.up);
        RaycastHit2D r = Physics2D.Raycast(fovPoint.transform.position, dir, range);

        if (angle < fovAngle / 2)  //Si el angulo es menor que 90 /2 ?????
        {
            if (r.collider.CompareTag("Player"))
            {
                // WE SPOTTED THE PLAYER
                Debug.Log("SEEN!");
                Debug.DrawRay(fovPoint.transform.position, dir, Color.red);
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
