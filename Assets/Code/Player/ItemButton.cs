using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    ItemType itemType;

    public void ChangeWeapon(int id)
    {
        if (GameManager.instance.itemToHold == null)
            return;
        if (itemType != GameManager.instance.itemToHold.itemType)
            return;
        if (itemType == ItemType.Weapon)
        {
            inventory.ChangeWeapon(id);
        }
        else
        {
            inventory.ChangeArmor(id);
        }
    }
}
