using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Analytics;

public class EnemyPool : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> enemyQueue = new Dictionary<string, Queue<GameObject>>();
    [SerializeField]
    GameObject[] prefabs;
    [SerializeField]
    List<string> names;

    private void Start()
    {
        foreach (var pref in prefabs)
        {
            var tempParent = Instantiate(new GameObject(pref.name));
            names.Add(pref.name);
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < 20; i++)
            {
                GameObject obj = Instantiate(pref);
                obj.transform.SetParent(tempParent.transform, false);
                queue.Enqueue(obj);
            }
            enemyQueue.Add(pref.name, queue);
        }
        GameManager.instance.numberOfEnemies = 0;
    }

    public GameObject GetRandomEnemy(Vector3 position)
    {
        return GetEnemy(names[Random.Range(0, names.Count)], position);
    }
    public GameObject GetEnemy(string tag, Vector3 position)
    {
        if(!enemyQueue.ContainsKey(tag))
        {
            Debug.LogError("Tag not found");
            return null;
        }
        GameObject temp = enemyQueue[tag].Dequeue();
        temp.transform.position = position;
        temp.SetActive(true);

        enemyQueue[tag].Enqueue(temp);

        return temp;
    }

}
