using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductoPunto : MonoBehaviour
{

    [SerializeField] Transform player;  //posicion player
    [SerializeField] Vector2 objectVector;
    [SerializeField] Vector2 pointToPlayer; //Vector entre el punto y el player
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        objectVector = Vector2.up;
    }

    // Update is called once per frame
    void Update()
    {
        pointToPlayer = (player.position-transform.position).normalized;
        float dotProduct =Vector2.Dot(objectVector, pointToPlayer);
        Debug.Log(dotProduct);

        if (dotProduct > 0) { gameObject.GetComponentInParent<SpriteRenderer>().sortingLayerName = "New Layer 1"; }
        else { gameObject.GetComponentInParent<SpriteRenderer>().sortingLayerName = "New Layer 2"; }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, player.position);
        
    }
}
