using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keys_PickUp : MonoBehaviour
{
    [SerializeField] int keyOperator;

    [SerializeField] GameObject[] keyObjectOff;

    [SerializeField] Sprite[] keySpritesOn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddKey(keyOperator);
            keyObjectOff[keyOperator].GetComponent<Image>().sprite = keySpritesOn[keyOperator];
            Destroy(gameObject);
        } 
    }
}
