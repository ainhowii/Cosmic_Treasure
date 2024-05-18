using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTarget : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] CinemachineVirtualCamera vcam;
    [SerializeField] Transform player;
    [SerializeField] float threshold; //La cantidad de desplazamiento
    [SerializeField] float mapThreshold; //Desplazamiento en modo mapa
    float initialThreshold;
    float initialOrtho;
    
    float t;
    bool mapEnabled;


    private void Start()
    {
        initialThreshold = threshold;
        mapEnabled = false;
        initialOrtho = 12f;
        if (vcam.m_Lens.OrthographicSize != initialOrtho) { vcam.m_Lens.OrthographicSize = initialOrtho; }
    }

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //Checkea pos mouse
        Vector3 targetPos = new Vector3((player.position.x + mousePos.x) , (player.position.y + mousePos.y))/2f; //Calcula la distancia entre player y mousepos

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x); //Constraint (Value, lower threshold, upper threshold
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

        this.transform.position = targetPos; //La pos de la camara = la pos del mouse ??

        if (Input.GetKeyDown(KeyCode.Space) && !mapEnabled)
        {
            ToMap();   
        }
        else if (Input.GetKeyDown(KeyCode.Space) && mapEnabled)
        {
            FromMap();
        }

        t =  Time.time;
    }

    void ToMap()
    {
        vcam.m_Lens.OrthographicSize = Mathf.Lerp(initialOrtho,50,t);
        mapEnabled = true;
        threshold = mapThreshold;
    }
    void FromMap()
    {
        vcam.m_Lens.OrthographicSize = Mathf.Lerp(50, initialOrtho, t);
        mapEnabled = false;
        threshold = initialThreshold;
    }
}
