using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Keys_PickUp : MonoBehaviour
{
    [SerializeField] int keyOperator;

    [SerializeField] GameObject[] keyObjectOff;

    [SerializeField] Sprite[] keySpritesOn;
    [SerializeField] string[] keysAnim;
    Animator anim;
    [SerializeField] Light2D keyLight;

    private void Start()
    {
        anim = GetComponent<Animator>();

        //keysAnim[0] = new string("KeyAnim");
            
        
        //keysAnim[4] = new string("PurpleKeyAnim");
        anim.Play(keysAnim[keyOperator]);
        Debug.Log("Anim " + keysAnim[keyOperator]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddKey(keyOperator);
            keyObjectOff[keyOperator].GetComponent<Image>().sprite = keySpritesOn[keyOperator];
            Destroy(gameObject);
            keyLight.intensity = 0;
        } 
    }
}
