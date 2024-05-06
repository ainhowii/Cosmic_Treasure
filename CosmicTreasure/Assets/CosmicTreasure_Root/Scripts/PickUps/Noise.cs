using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Noise : MonoBehaviour
{
    public GameObject noisePrefab;
    public GameObject enemy;
    public GameObject noise;
    public float speed;
    public float fireNoise = 0;
    private bool isDropped;
    [SerializeField] private float radius;

    public Transform firePoint;

    EnemyTest change;

    // Start is called before the first frame update
    void Start()
    {
        isDropped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) //&& equippedNoise)
        {
            NoiseAction();
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            NoiseAction2();
        }
    }

    private void NoiseAction()   //Instancia el dispositivo
    {
        GameObject bullet = Instantiate(noisePrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireNoise, ForceMode2D.Impulse);
        isDropped = true;
        
    }

    private void NoiseAction2()   //Activa el dispositivo y los enemigos van a él
    {
        //PlaySFX
        GameObject.FindGameObjectsWithTag("enemy");
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, noise.transform.position, speed * Time.deltaTime);
        Invoke("Return", 10);
    }

    private void Return()  //El objeto luego de unos segundos se destruye
    {
        Destroy(gameObject);
    }
}
