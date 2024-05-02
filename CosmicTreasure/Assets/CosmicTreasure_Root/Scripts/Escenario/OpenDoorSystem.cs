using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorSystem : MonoBehaviour
{
    Animator anim;
    Collider2D col;
    
    // Start is called before the first frame update
    void Start()
    {
       anim = gameObject.GetComponentInParent<Animator>(); 
        col = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.Play("DoorOpenAnim");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.Play("DoorCloseAnim");
        }
    }
}
