using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [Header("Weapons")]
    public GameObject[] weaponSlots;
    [SerializeField]
    Image[] weaponImages;
    [Header("Armor")]
    [SerializeField]
    Image[] chestplateImage;
    [SerializeField]
    Image[] helmetImage;
    [SerializeField]
    Image[] leggingsImage;
    [SerializeField]
    Image[] bootsImage;
    [SerializeField]
    Image[] ringImages;
    [Header("Misc")]
    [SerializeField]
    Shop shop;
    [SerializeField]
    Stats playerStats;
    Image[] _targetImages;

    public void AddItem()
    {
        switch(GameManager.instance.itemToHold.itemType)
        {
            case ItemType.Weapon:
                if (AddWeapon())
                    return;
                break;
            case ItemType.Chestplate:
                if (AddArmor(chestplateImage))
                    return;
                break;
            case ItemType.Helmet:
                if (AddArmor(helmetImage))
                    return;
                break;
            case ItemType.Leggings:
                if (AddArmor(leggingsImage))
                    return;
                break;
            case ItemType.Boots:
                if (AddArmor(bootsImage))
                    return;
                break;
            case ItemType.Ring:
                if (AddArmor(ringImages))
                    return;
                break;
            default:
                break;
        }
        shop.InventoryFull();
    }
    void ModifyItem(Image targetImage)
    {
        targetImage.sprite = GameManager.instance.itemToHold.GetComponent<Item>().image;
        targetImage.name = GameManager.instance.itemToHold.GetComponent<Item>().name;
        targetImage.enabled = true;
        _targetImages = null;
        GameManager.instance.CloseShop();
    }

    bool AddArmor(Image[] targetItemImgs)
    {
        _targetImages = targetItemImgs;
        for (int i = 0; i < targetItemImgs.Length; i++)
        {
            if(targetItemImgs[i].enabled == false)
            {
                ChangeArmor(i);
                return true;
            }
        }

        return false;
    }

    public void ChangeArmor(int slot)
    {
        var item = GameManager.instance.itemToHold;
        playerStats.UpdateHealthValues(item.maxHealth,item.regenHealth);
        playerStats.UpdateUtilityValues(item.movementSpeed,item.dodgeChance,item.luck);
        playerStats.UpdateDamageValues(item.attackDamage,item.attackSpeed,item.critChance,item.critDamage,item.lifeSteal);
        if (_targetImages[slot].enabled == true)
            RemoveItem(shop.GetItem(_targetImages[slot].name));
        ModifyItem(_targetImages[slot]);
    }
    
    void RemoveItem(Item item)
    {
        playerStats.UpdateHealthValues(-item.maxHealth, -item.regenHealth);
        playerStats.UpdateUtilityValues(-item.movementSpeed, -item.dodgeChance, -item.luck);
        playerStats.UpdateDamageValues(-item.attackDamage, -item.attackSpeed, -item.critChance, -item.critDamage, -item.lifeSteal);
    }

    bool AddWeapon()
    {
        for (int i = 0; i < weaponImages.Length; i++)
        {
            if (weaponImages[i].GetComponent<Image>().enabled == false)
            {
                ChangeWeapon(i);
                return true;
            }
        }

        return false;
    }

    public void ChangeWeapon(int slot)
    {
        if (weaponSlots[slot].transform.childCount>0)
            Destroy(weaponSlots[slot].transform.GetChild(0).gameObject);
        GameObject temp = Instantiate(GameManager.instance.itemToHold.gameObject);
        temp.transform.SetParent(weaponSlots[slot].transform, false);
        ModifyItem(weaponImages[slot]);
    }
}
