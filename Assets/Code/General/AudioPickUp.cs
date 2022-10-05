using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPickUp : MonoBehaviour
{
    private void Start()
    {
        AudioListener.volume = .3f;// PlayerPrefs.GetFloat("volume");
    }
}
