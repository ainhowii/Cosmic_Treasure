using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTarget : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] Transform player;
    [SerializeField] float threshold; //La cantidad de desplazamiento
    
    
   

    
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //Checkea pos mouse
        Vector3 targetPos = new Vector3((player.position.x + mousePos.x) , (player.position.y + mousePos.y))/2f; //Calcula la distancia entre player y mousepos

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x); //Constraint (Value, lower threshold, upper threshold
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

        this.transform.position = targetPos; //La pos de la camara = la pos del mouse ??
    }
}
