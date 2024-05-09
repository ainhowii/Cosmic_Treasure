using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class OpenDoorSystem : MonoBehaviour
{
    Animator anim;
    Collider2D col;
    [SerializeField] GameObject roof;
    SpriteShapeRenderer shapeRenderer;
    [SerializeField] GameObject closeCol;
    
    // Start is called before the first frame update
    void Start()
    {
       anim = gameObject.GetComponentInParent<Animator>(); 
        col = gameObject.GetComponent<Collider2D>();
        shapeRenderer = roof.GetComponent<SpriteShapeRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.keyChain[1] == true)
        {
            anim.Play("DoorOpenAnim");
            shapeRenderer.color = new Color(1f,1f,1f,0f);
            closeCol.GetComponent<Collider2D>().enabled = false;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.keyChain[1] == true)
        {
            anim.Play("DoorCloseAnim");
        }
    }
}
