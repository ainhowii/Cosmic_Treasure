using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour
{
    Renderer ren;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) //&& equippedTrans)
        {
            TransAction();
        }
    }

    private void TransAction()  //Cambia la transparencia del player
    {
        ren = GetComponent<Renderer>();
        Color customColor = new Color(0.18f, 0.21f, 0.83f, 0.45f);
        Invoke("Return", 10);
    }

    private void Return()  //Vuelve a ser opaco el player
    {

    }
}
