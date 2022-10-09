using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject[] weaponSlots;
    [SerializeField]
    Image[] weaponImages;
    [SerializeField]
    Shop shop;

    public void AddItem(Item item)
    {
        switch(item.itemType)
        {
            case ItemType.Weapon:
                if (AddWeapon(item))
                    return;
                break;
            default:
                break;
        }

        if (AddWeapon(item))
            return;
        GameManager.instance.itemToHold = item;
        shop.InventoryFull();
    }

    bool AddWeapon(Item weapon)
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i].transform.childCount == 0)
            {
                ModifyWeapon(i, weapon);
                return true;
            }
        }
        return false;
    }

    void ModifyWeapon(int slot, Item item)
    {
        GameObject temp = Instantiate(item.gameObject);
        temp.transform.SetParent(weaponSlots[slot].transform, false);
        weaponImages[slot].sprite = temp.GetComponent<Throwable>().image;
        weaponImages[slot].enabled = true;
        GameManager.instance.CloseShop();
        GameManager.instance.itemToHold = null;
    }

    public void ChangeItem(int slot)
    {
        Destroy(weaponSlots[slot].transform.GetChild(0).gameObject);
        ModifyWeapon(slot, GameManager.instance.itemToHold);
    }
}
