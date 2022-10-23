using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Shop shop;
    public void OnPointerEnter(PointerEventData eventData)
    {
        var item = shop.GetItem(transform.GetChild(0).name);
        if (item == null)
            return;
        TooltipSystem.item = shop.GetItem(transform.GetChild(0).name);
        TooltipSystem.Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
