using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float speedMovement;

    [SerializeField] private Transform[] movementPoints;

    [SerializeField] private float minimumDistance;

    private int randomNumber;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        randomNumber = Random.Range(0, movementPoints.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Rotate();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movementPoints[randomNumber].position, speedMovement * Time.deltaTime);

        if (Vector2.Distance(transform.position, movementPoints[randomNumber].position) < minimumDistance)
        {
            randomNumber = Random.Range(0, movementPoints.Length);
            //Rotate();
        }
    }

    /*
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
    */
}
