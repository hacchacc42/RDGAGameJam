using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject playerSpawnLocation;
    

    private void OnEnable()
    {
        if(GameManager.instance!=null)
            GameManager.instance.player.transform.position = playerSpawnLocation.transform.position;
    }
}
