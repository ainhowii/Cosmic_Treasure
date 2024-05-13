using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ParalelosProdPunto : MonoBehaviour
{
    [SerializeField] Transform player;  //posicion player
    [SerializeField] Vector2 objectVector1; //Define la linea divisoria NE-SO
    [SerializeField] Vector2 objectVector2; //Define la linea divisoria NO-SE
    [SerializeField] Vector2 pointToPlayer; //Vector entre el punto y el player
    [SerializeField] float playerDistance;
    [SerializeField] float maxDistance;
    [SerializeField] int keyNum;            //Define la llave de la puerta que permite transparencia
    [SerializeField] string defaultLayer;
    [SerializeField] string actionLayer;
    [SerializeField] float actionAlpha;
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
        //Debug.Log("Distance = " + playerDistance);

        // Debug.Log("pp1 = " + dotProduct1);
        // Debug.Log("pp2 = " + dotProduct2);

        if (GameManager.Instance.keyChain[keyNum]) //Si tiene la llave
        {
            if (dotProduct1 > 0 && dotProduct2 > 0) //Si player está detrás
            {
                gameObject.GetComponentInParent<SpriteRenderer>().sortingLayerName = actionLayer;   //.sortingLayerName = "FrontLayer1";
                gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, actionAlpha);
                //Debug.Log("Detrás");
                if (playerDistance > maxDistance) { gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1); }
            }
            else if (dotProduct1 < 0 && dotProduct2 > 0) //Si player está delante
            {
                gameObject.GetComponentInParent<SpriteRenderer>().sortingLayerName = defaultLayer;//.sortingLayerName = "BackLayer1";
                gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                //Debug.Log("Delante");
            }
            else if (dotProduct1 < 0 && dotProduct2 < 0) //Si player delante a la izq
            {
                gameObject.GetComponentInParent<SpriteRenderer>().sortingLayerName = actionLayer;//.sortingLayerName = "FrontLayer1";
                gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                //Debug.Log("DelanteIzq");
            }
            else if (dotProduct1 > 0 && (dotProduct2 < 0)) //Si player detras a la izq
            {
                gameObject.GetComponentInParent<SpriteRenderer>().sortingLayerName = actionLayer;//.sortingLayerName = "FrontLayer1";
                gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
                //Debug.Log("DetrásIzq");
                if (transform.position.x < player.transform.position.x && playerDistance < maxDistance)       //Prueba: Usar la distancia para entrar a los IF
                {
                    gameObject.GetComponentInParent<SpriteRenderer>().color = new Color(1, 1, 1, actionAlpha);
                }
            }
        }
    }
}
