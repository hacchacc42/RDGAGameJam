using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public Sprite image;
    [SerializeField]
    GameObject prefab;
    Queue<GameObject> weaponsQueue;
    [Header("Stats")]
    [SerializeField]
    float attackSpeed;
    [SerializeField]
    GameObject throwParent;
    [SerializeField]
    AudioSource audioSource;
    private void OnEnable()
    {
        throwParent = new GameObject("Throw parent");
        weaponsQueue = new Queue<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(prefab);
            temp.transform.SetParent(throwParent.transform, false);
            temp.SetActive(false);
            weaponsQueue.Enqueue(temp);
        }
        StartCoroutine(C_Throwing());
    }

    IEnumerator C_Throwing()
    {
        PlayAudio();
        GameObject temp = weaponsQueue.Dequeue();
        temp.transform.position = transform.position;
        temp.transform.rotation = transform.rotation;
        temp.SetActive(true);
        weaponsQueue.Enqueue(temp);
        yield return new WaitForSeconds( 1.0f/(attackSpeed * GameManager.instance.attackSpeedMultiplier));
        StartCoroutine(C_Throwing());
    }
    private void OnDisable()
    {
        Destroy(throwParent);
    }

    void PlayAudio()
    {
        audioSource.volume = Random.Range(0.1f, .2f);
        audioSource.pitch = Random.Range(0.8f, 1.1f);
        audioSource.Play();
    }
}
