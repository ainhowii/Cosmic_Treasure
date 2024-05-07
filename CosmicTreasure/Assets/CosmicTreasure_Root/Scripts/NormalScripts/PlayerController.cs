using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;

    public SpriteRenderer spriteRenderer;

    public List<Sprite> nSprites;
    public List<Sprite> neSprites;
    public List<Sprite> eSprites;
    public List<Sprite> seSprites;
    public List<Sprite> sSprites;

    public float walkSpeed;

    public float frameRate;

    float idleTime;

    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;


    Vector2 direction;

    private void Awake()
    {
        inventory = new Inventory(UseItem);
        uiInventory.SetInventory(inventory);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            //Touching item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Noise:
                //FlashGreen();                                               //AQUI IRIA EL METODO DE LO QUE HACE EL OBJETO
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Noise, amount = 1 });
                break;
            case Item.ItemType.Charge:
                //FlashBlue();                                                //AQUI TAMBIEN
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Charge, amount = 1 });
                break;
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        body.velocity = direction * walkSpeed;

        HandleSpriteFlip();
        SetSprite();
    }

    void SetSprite()
    {
        List<Sprite> directionSprites = GetSpriteDirection();

        if (directionSprites != null)
        { //holding a direction

            float playTime = Time.time - idleTime;
            int totalFrames = (int)(playTime * frameRate);
            int frame = totalFrames % directionSprites.Count;

            spriteRenderer.sprite = directionSprites[frame];
        }
        else
        {
            idleTime = Time.time;
        }

    }

    void HandleSpriteFlip()
    {
        if (!spriteRenderer.flipX && direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (spriteRenderer.flipX && direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }

    }

    List<Sprite> GetSpriteDirection()
    {
        List<Sprite> selectedSprites = null;

        if (direction.y > 0) //North
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = neSprites;
            }
            else //neutral X
            {
                selectedSprites = nSprites;
            }
        }
        else if (direction.y < 0) //South
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = seSprites;
            }
            else
            {
                selectedSprites = sSprites;
            }
        }
        else //neutral
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = eSprites;
            }
        }

        return selectedSprites;

    }

   

}
