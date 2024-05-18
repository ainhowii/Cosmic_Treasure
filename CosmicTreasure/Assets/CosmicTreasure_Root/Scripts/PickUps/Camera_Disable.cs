using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Disable : MonoBehaviour
{
    PlayerController controller;

    private void CameraOff()
    {
        if (controller.isInRange)
        {
            //La camara deja de moverse y no puede detectar al jugador
        }
    }
}
