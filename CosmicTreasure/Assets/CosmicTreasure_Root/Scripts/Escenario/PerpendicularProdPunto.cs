using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerpendicularProdPunto : MonoBehaviour
{
    [SerializeField] Transform player;  //posicion player
    [SerializeField] Vector2 objectVector1; //Define la linea divisoria NE-SO
    [SerializeField] Vector2 objectVector2; //Define la linea divisoria NO-SE
    [SerializeField] Vector2 pointToPlayer; //Vector entre el punto y el player
    [SerializeField] float playerDistance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        objectVector1 = new Vector2(2, 4).normalized;
        objectVector2 = new Vector2(2, -4).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        pointToPlayer = (player.position - transform.position).normalized;
        float dotProduct1 = Vector2.Dot(objectVector1, pointToPlayer);  //ProductoPunto que divide NE-SO
        float dotProduct2 = Vector2.Dot(objectVector2, pointToPlayer);  //ProductoPunto que divide NO-SE
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log("Distance = " + playerDistance);

        // Debug.Log("pp1 = " + dotProduct1);
        // Debug.Log("pp2 = " + dotProduct2);

        if (dotProduct1 < 0 && dotProduct2 < 0) //Si player est� detr�s
        {
            gameObject.GetComponentInParent<SpriteRenderer>().sortingLayerName = "New Layer 1";
            gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
            Debug.Log("Detr�s");
            if (playerDistance > 6) { gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1); }
        }
        else if (dotProduct1 < 0 && dotProduct2 > 0) //Si player est� delante
        {
            gameObject.GetComponentInParent<SpriteRenderer>().sortingLayerName = "New Layer 2";
            gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            Debug.Log("Delante");
        }
        else if (dotProduct1 > 0 && dotProduct2 > 0) //Si player delante a la der
        {
            gameObject.GetComponentInParent<SpriteRenderer>().sortingLayerName = "New Layer 2";
            gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            Debug.Log("DelanteIzq");
        }
        else if (dotProduct1 > 0 && (dotProduct2 < 0)) //Si player detras a la der
        {
            gameObject.GetComponentInParent<SpriteRenderer>().sortingLayerName = "New Layer 1";
            gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            Debug.Log("Detr�sIzq");
            if (transform.position.x > player.transform.position.x && playerDistance < 6)       //Prueba: Usar la distancia para entrar a los IF
            {
                gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
            }
        }
    }
}