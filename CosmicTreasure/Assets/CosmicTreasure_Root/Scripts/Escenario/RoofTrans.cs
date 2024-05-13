using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofTrans : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] private float transparency;
    // Start is called before the first frame update
    void Start()
    {
       sprite = gameObject.GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { sprite.color = new Color(1, 1, 1, transparency); }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { sprite.color = new Color(1, 1, 1, 1); }
    }
}
