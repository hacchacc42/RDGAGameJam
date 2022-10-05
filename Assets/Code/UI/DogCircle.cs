using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DogCircle : MonoBehaviour
{
    Image image;
    float timer = 0;
    private void Start()
    {
        timer = 1;
        image = GetComponent<Image>();
    }

    public void ResetTimer()
    {
        timer = 0;
        image.fillAmount = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime/8.0f;
        image.fillAmount=Mathf.Lerp(0,1,timer);
    }
}
