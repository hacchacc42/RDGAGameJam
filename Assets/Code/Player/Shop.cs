using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    GameObject changingWindow;
    [SerializeField]
    Throwable[] weapons;
    [SerializeField]
    ShopWeapon[] shopWeapons;

    private void OnEnable()
    {
        Time.timeScale = 0;
        GenerateNewWeapons();
    }

    public void GenerateNewWeapons()
    {
        foreach(var shopWeapon in shopWeapons)
        {
            shopWeapon.ReceiveItem(weapons[Random.Range(0, weapons.Length)]);
        }
    }
    public void InventoryFull()
    {
        changingWindow.SetActive(true);
    }
}
