using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
                case ItemType.Noise:         return ItemAssets.Instance.noiseSprite;
                case ItemType.Charge:        return ItemAssets.Instance.chargeSprite;
                case ItemType.Key:           return ItemAssets.Instance.keySprite;
        }
    }

    public Color GetColor()
    {
        switch (itemType)
        {
            default:
            case ItemType.Noise:     return new Color(0, 0, 0);
            case ItemType.Charge:    return new Color(1, 0, 0);
            case ItemType.Key:       return new Color(0, 0, 1);
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Noise:
            case ItemType.Charge:
                return true;            //Es stackable
            case ItemType.Key:
                return false;           //NO es stackable
        }
    }

}
