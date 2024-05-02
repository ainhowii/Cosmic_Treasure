using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] float speed = 20f;

    [SerializeField] float distance = 10f;

    [SerializeField] float soundVolume = 0.5f;

    RaycastHit2D hit;

    //AudioSource sfx;

    private void Start()
    {
        //sfx = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        hit = Physics2D.Raycast(transform.position, transform.right, distance);
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, hit.point, Color.white);
            Debug.Log("Did Hit");
            //sfx.volume = soundVolume;
            //sfx.Play();
            Vector3 vv = hit.point;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.position + transform.right * distance, Color.black);
            Debug.Log("Did not hit");
        }
    }
}
