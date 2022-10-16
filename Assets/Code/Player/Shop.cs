using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    GameObject changingWindow;
    [SerializeField]
    Item[] items;
    [SerializeField]
    ShopItems[] shopItems;

    private void OnEnable()
    {
        Time.timeScale = 0;
        GenerateNewWeapons();
    }

    public void GenerateNewWeapons()
    {
        foreach(var shopItem in shopItems)
        {
            shopItem.ReceiveItem(items[Random.Range(0, items.Length)]);
        }
    }
    public void InventoryFull()
    {
        changingWindow.SetActive(true);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        changingWindow.SetActive(false);
    }

    public Item GetItem(string itemName)
    {
        for(int i=0; i < items.Length; i++)
        {
            if (items[i].name==itemName)
            {
                return items[i];
            }
        }
        return null;
    }

}
