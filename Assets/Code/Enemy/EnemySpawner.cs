using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Animator animator;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DelayedStart());
        GameManager.instance.endOfWaveEvent.AddListener(StopSpawning);
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        animator.SetTrigger("Spawn");
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.numberOfEnemies++;
        GameManager.instance.enemyPool.GetRandomEnemy(transform.position);
        yield return new WaitForSeconds(Random.Range(4.0f,7.0f));
        StartCoroutine(SpawnEnemy());
    }

    public void StopSpawning()
    {
        animator.SetTrigger("Dissapear");
        StopAllCoroutines();
    }
}
