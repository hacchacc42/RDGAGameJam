using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    [SerializeField]
    Inventory inventory;

    public void ChangeWeapon(int id)
    {
        if (GameManager.instance.itemToHold != null)
        {
            inventory.ChangeWeapon(id);
        }
    }
}
