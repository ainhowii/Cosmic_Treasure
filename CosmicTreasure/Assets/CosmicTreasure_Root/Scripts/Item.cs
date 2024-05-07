using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    public enum ItemType
    {
        Noise,
        Charge,
        Key,
    }

    public ItemType itemType;
    public int amount;

}
