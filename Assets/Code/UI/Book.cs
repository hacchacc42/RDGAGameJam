using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField]
    GameObject books;
    private void OnEnable()
    {
        Time.timeScale = 0;
    }


    public void CloseBook()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        books.SetActive(false);
    }
}
