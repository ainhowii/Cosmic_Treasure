using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
      itemList = new List<Item>();

      AddItem(new Item { itemType = Item.ItemType.Noise, amount = 1 });
      Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }
}
