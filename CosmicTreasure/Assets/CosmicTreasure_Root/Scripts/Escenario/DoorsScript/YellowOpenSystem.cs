using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowOpenSystem : MonoBehaviour
{
    Animator anim;
    Collider2D col;
    //[SerializeField] GameObject roof;
    //SpriteRenderer shapeRenderer;
    [SerializeField] GameObject closeCol;
    [SerializeField] int keyToOpen;


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponentInParent<Animator>();
        col = gameObject.GetComponent<Collider2D>();
        //shapeRenderer = roof.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.keyChain[keyToOpen] == true)
        {
            anim.Play("DoorYellowOpenAnim");
            //shapeRenderer.color = new Color(1f, 1f, 1f, 0f);         //Apaga el techo
            closeCol.GetComponent<Collider2D>().enabled = false;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.keyChain[keyToOpen] == true)
        {
            anim.Play("DoorYellowCloseAnim");
        }
    }
}
