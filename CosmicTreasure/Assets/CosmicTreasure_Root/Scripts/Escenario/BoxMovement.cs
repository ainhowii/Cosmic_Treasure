using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    private int i; //Índice del array de puntos para que la plataforma detecte un punto a seguir.
    private float boxSpeed;
    [SerializeField] Vector2[] points;
    [SerializeField] int startingPoint;

    // Start is called before the first frame update
    void Start()
    {
        boxSpeed = 2f;
        transform.position = points[startingPoint];
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i]) < 0.02f)
        {
            i++; //Aumenta el índice, cambia de objetivo hacia el que moverse.
            if (i == points.Length) //Chequea si la plataforma ha llegado al último punto del array.
            {

                i = 0; //Resetea el índice para volver a empezar, la plataforma va hacia el punto 0.
                transform.position = points[startingPoint];
                

            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i], boxSpeed * Time.deltaTime);
    }
}
