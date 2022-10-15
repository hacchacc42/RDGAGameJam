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

    public void AddItem()
    {
        if(GameManager.instance.itemToHold.itemType == ItemType.Weapon)
        {
            if (AddWeapon())
                return;
        }
        else
        {

        }
        shop.InventoryFull();
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

    void ModifyItem(int slot)
    {
        weaponImages[slot].sprite = GameManager.instance.itemToHold.GetComponent<Throwable>().image;
        weaponImages[slot].enabled = true;
        GameManager.instance.CloseShop();
    }

    public void ChangeWeapon(int slot)
    {
        if (weaponSlots[slot].transform.childCount>0)
            Destroy(weaponSlots[slot].transform.GetChild(0).gameObject);
        GameObject temp = Instantiate(GameManager.instance.itemToHold.gameObject);
        temp.transform.SetParent(weaponSlots[slot].transform, false);
        ModifyItem(slot);
    }
}
