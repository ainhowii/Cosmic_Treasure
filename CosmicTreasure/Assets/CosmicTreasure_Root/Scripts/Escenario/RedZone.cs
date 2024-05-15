using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZone : MonoBehaviour
{
    Collider2D col;
    SpriteRenderer sprite;
    private bool isSpawned;
    
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       if (isSpawned == false) { StartCoroutine("RedZoneSpawn"); } 
    }

    IEnumerator RedZoneSpawn()
    {
        isSpawned = true;
        sprite.color = new Color(1,1,1,1);
        yield return new WaitForSeconds(.5f);
        sprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(.5f);
        sprite.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(.5f);
        sprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(.5f);
        sprite.color = new Color(1, 1, 1, 1);
        col.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        col.gameObject.SetActive(false);
        isSpawned = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { }
    }
}
