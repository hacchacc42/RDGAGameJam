using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    GameObject changingWindow;
    [SerializeField]
    ShopItems[] shopItems;
    [Header("Items Rarities")]
    [SerializeField]
    Item[] tutorialitems;
    [SerializeField]
    Item[] commonItems;
    [SerializeField]
    Item[] uncommonItems;
    [SerializeField]
    Item[] rareItems;
    [SerializeField]
    Item[] epicItems;
    [SerializeField]
    Item[] legendaryItems;

    private void OnEnable()
    {
        Time.timeScale = 0;
        GenerateNewWeapons();
    }

    public void GenerateNewWeapons()
    {
        foreach(var shopItem in shopItems)
        {
            shopItem.ReceiveItem(PickRarityItem());
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

    private Item PickRarityItem()
    {
        Item[] itemRarity = tutorialitems;
        int playerLevel = GameManager.instance.GetComponent<PlayerLevel>().level;
        if(playerLevel==1)
        {
            return itemRarity[Random.Range(0, itemRarity.Length)];
        }
        int luck = playerLevel + GameManager.instance.player.GetComponent<Stats>().luck;
        float dropChance = Random.Range(1, 100.0f) / Mathf.Pow(1.01f,playerLevel) / Mathf.Pow(1.02f, luck);
        if(dropChance<1.5f)
        {
            itemRarity = legendaryItems;
        }
        else if(dropChance<4)
        {
            itemRarity = epicItems;
        }
        else if(dropChance<12)
        {
            itemRarity = rareItems;
        }
        else if(dropChance<27)
        {
            itemRarity = uncommonItems;
        }
        else
        {
            itemRarity = commonItems;
        }
        return itemRarity[Random.Range(0, itemRarity.Length)];
    }

    public Item GetItem(string itemName)
    {
        var items = tutorialitems
            .Concat(commonItems)
            .Concat(uncommonItems)
            .Concat(rareItems)
            .Concat(epicItems)
            .Concat(legendaryItems)
            .ToArray();

        for (int i=0; i < items.Length; i++)
        {
            if (items[i].name==itemName)
            {
                return items[i];
            }
        }
        return null;
    }

}
