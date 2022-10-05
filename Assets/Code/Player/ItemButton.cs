using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    [SerializeField]
    Inventory inventory;

    public void ChangeItem(int id)
    {
        if (GameManager.instance.itemToHold != null)
        {
            inventory.ChangeItem(id);
        }
    }
}
