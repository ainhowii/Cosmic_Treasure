using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndUse : MonoBehaviour
{
    public List<GameObject> takenObjects = new List<GameObject>(); // Lista para almacenar los objetos cogidos

    // M�todo para coger un objeto
    public void GrabObject(GameObject objects)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            takenObjects.Add(objects); // Agrega el objeto a la lista de objetos cogidos
            objects.SetActive(false); // Desactiva el objeto para "cogerlo"
        }
    }

    // M�todo para utilizar un objeto al presionar un bot�n
    public void UseObject()
    {
        if (takenObjects.Count > 0)
        {
            // Obt�n el �ltimo objeto de la lista
            GameObject UsingObject = takenObjects[takenObjects.Count - 1];

            // Haz lo que necesites con el objeto a utilizar
            UsingObject.SetActive(true);

            // Elimina el objeto utilizado de la lista
            takenObjects.RemoveAt(takenObjects.Count - 1);
        }
    }
}
