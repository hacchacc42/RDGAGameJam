using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public Sprite image;
    [SerializeField]
    Projectile prefab;
    Queue<Projectile> weaponsQueue;
    [SerializeField]
    GameObject throwParent;
    [SerializeField]
    AudioSource audioSource;
    [Header("Stats")]
    [SerializeField]
    float attackSpeed;
    [SerializeField]
    int damage;
    [SerializeField]
    float projectileSpeed;
    [SerializeField]
    float range;
    private void OnEnable()
    {
        throwParent = new GameObject("Throw parent");
        weaponsQueue = new Queue<Projectile>();
        for (int i = 0; i < 10; i++)
        {
            Projectile temp = Instantiate(prefab);
            temp.transform.SetParent(throwParent.transform, false);
            temp.gameObject.SetActive(false);
            temp.SetStats(damage, range, projectileSpeed);
            weaponsQueue.Enqueue(temp);
        }
        StartCoroutine(C_Throwing());
    }

    IEnumerator C_Throwing()
    {
        PlayAudio();
        Projectile temp = weaponsQueue.Dequeue();
        temp.transform.position = transform.position;
        temp.transform.rotation = transform.rotation;
        temp.gameObject.SetActive(true);
        weaponsQueue.Enqueue(temp);
        yield return new WaitForSeconds(1.0f / attackSpeed);
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
