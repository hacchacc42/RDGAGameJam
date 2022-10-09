using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField]
    GameObject books;
    [SerializeField]
    bool endingBook = false;
    private void OnEnable()
    {
        if (endingBook)
            return;
        Time.timeScale = 0;
    }


    public void CloseBook()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        books.SetActive(false);
    }
}
