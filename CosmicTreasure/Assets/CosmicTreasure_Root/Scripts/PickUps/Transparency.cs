using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour
{
    SpriteRenderer ren;
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
        ren = GetComponent<SpriteRenderer>();
        ren.color = new Color(1f, 1f, 1f, 0.40f);
        Invoke("Return", 5);
    }

    private void Return()  //Vuelve a ser opaco el player
    {
        ren.color = new Color(1f, 1f, 1f, 1f);
    }
}
