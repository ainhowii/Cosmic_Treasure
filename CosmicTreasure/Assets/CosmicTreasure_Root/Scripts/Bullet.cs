using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    private Vector2 playerPosShoot;
    GameObject player;



    private void Start()
    {
        //player.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosShoot = player.transform.position;
        
    }

    private void Update()
    {
        
        
        //transform.position = Vector2.MoveTowards(transform.position, playerPosShoot, speed * Time.deltaTime);
    }
}
