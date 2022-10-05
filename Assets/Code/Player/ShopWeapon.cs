using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopWeapon : MonoBehaviour
{
    [SerializeField]
    GameObject weapon;
    [SerializeField]
    Inventory inventoy;
    [SerializeField]
    Image image;

    public void ReceiveItem(Throwable weapon)
    {
        this.weapon = weapon.gameObject;
        image.sprite = weapon.image;
    }

    public void TryAddWeapon()
    {
        inventoy.AddWeapon(weapon);
    }
}
