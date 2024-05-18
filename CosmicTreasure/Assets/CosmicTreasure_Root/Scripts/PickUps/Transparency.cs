using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour
{
    public EnemyTest enem;
    public Transform player;
    private Color newColor = new Color(1f, 1f, 1f, 0.35f);
    private Color oldColor = new Color(1f, 1f, 1f, 1f);

    private bool isInvisible;

    EnemyTest ray;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isInvisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Alpha1)) //&& equippedTrans)
        {
            isInvisible = true;

            if (isInvisible)
            {
                // Desactivar raycast del enemigo (que no pueda detectarte)
                TransAction();
            }
        }
        */
        
    }

    public void Use()   //Cambia la transparencia del player
    {
        Debug.Log("Destruyase");
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        //Destroy(gameObject);
        spriteRenderer.color = newColor;
        // QUE EL ENEMIGO NO PUEDA VER AL PLAYER

        //ren = GetComponent<SpriteRenderer>();
        //ren.color = new Color(1f, 1f, 1f, 0.35f);
        Invoke("Return", 5);

    }

    private void Return()  //Vuelve a ser opaco el player
    {
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        spriteRenderer.color = oldColor;
        //ren.color = new Color(1f, 1f, 1f, 1f);
        Destroy(gameObject);
    }
}
