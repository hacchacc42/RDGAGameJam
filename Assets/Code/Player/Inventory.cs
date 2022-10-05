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

    public void AddWeapon(GameObject weapon)
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {    
            if (weaponSlots[i].transform.childCount == 0)
            {
                ModifyItem(i, weapon);
                return;
            }
        }
        GameManager.instance.itemToHold = weapon;
        shop.InventoryFull();
    }

    void ModifyItem(int slot, GameObject weapon)
    {
        GameObject temp = Instantiate(weapon);
        temp.transform.SetParent(weaponSlots[slot].transform, false);
        weaponImages[slot].sprite = temp.GetComponent<Throwable>().image;
        weaponImages[slot].enabled = true;
        GameManager.instance.CloseShop();
        GameManager.instance.itemToHold = null;
    }

    public void ChangeItem(int slot)
    {
        Destroy(weaponSlots[slot].transform.GetChild(0).gameObject);
        ModifyItem(slot, GameManager.instance.itemToHold);
    }
}
