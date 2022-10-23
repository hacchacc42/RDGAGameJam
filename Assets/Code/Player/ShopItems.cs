using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItems : MonoBehaviour
{
    [SerializeField]
    Item item;
    [SerializeField]
    Inventory inventoy;
    [SerializeField]
    Image image;

    public void ReceiveItem(Item item)
    {
        this.item = item;
        image.sprite = item.image;
        image.name = item.name;
        item.SetBackgroundColor(GetComponent<Image>());
    }

    public void TryAddItem()
    {
        GameManager.instance.itemToHold = item;
        inventoy.AddItem();
    }
}
