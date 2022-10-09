using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public EnemyPool enemyPool;
    public Shop shop;

    public GameObject itemToHold;
    public GameObject healhPotionPrefab;

    public float audioSetting;
    [SerializeField]
    AudioSource audioSource;


    [SerializeField]
    GameObject winLevel;
    [SerializeField]
    GameObject[] books;
    [SerializeField]
    GameObject bookMain;

    public UnityEvent endOfWaveEvent;
    public UnityEvent clearedEnemiesEvent;

    private void Awake()
    {
        instance = this;
    }

    [Header("General")]
    public GameObject player;
    public int numberOfEnemies = 0;
    public int waveTimer = 0;
    
    [Header("Levels")]
    public int level = 0;
    [SerializeField]
    GameObject currentLevel;
    [SerializeField]
    GameObject tutorial1;
    [SerializeField]
    GameObject basicLevel;
    [SerializeField]
    GameObject[] bossLevels;

    [Header("UI")]
    [SerializeField]
    TMP_Text clockText;

    IEnumerator WaveTimer()
    {
        waveTimer--;
        clockText.text = waveTimer.ToString();
        yield return new WaitForSeconds(1f);
        if (waveTimer > 0)
        {
            StartCoroutine(WaveTimer());
        }
        else
        {
            if(numberOfEnemies==0)
            {
                clearedEnemiesEvent.Invoke();
            }
            endOfWaveEvent.Invoke();
        }

    }

    public void EnterNewLevel()
    {
        level++;
        currentLevel.SetActive(false);
        if (level == 1)
        {
            currentLevel = tutorial1;
        }
        else
        {
            if (level % 2 == 1)
            {
                currentLevel = bossLevels[(level / 2) - 1];
            }
            else
            {
                currentLevel = basicLevel;
            }
        }

        currentLevel.SetActive(true);
        waveTimer = level * 5;
        shop.gameObject.SetActive(true);
        StartCoroutine(WaveTimer());
    }

    public void DefeatedEnemy()
    {
        numberOfEnemies--;
        if (waveTimer == 0)
        {
            if (numberOfEnemies == 0)
            {
                clearedEnemiesEvent.Invoke();
            }
        }
        audioSource.volume = Random.Range(0.6f, .9f);
        audioSource.pitch = Random.Range(0.8f, 1.1f);
        audioSource.Play();
    }

    public void SpawnHealthPotion(Vector3 position)
    {
        int rand = Random.Range(0, 10);
        if(rand >1)
        {
            return;
        }
        var temp = Instantiate(healhPotionPrefab);
        temp.transform.position = position;
    }

    public void CloseShop()
    {
        shop.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    

    IEnumerator Winning()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main Menu");
    }

    public void WinGame()
    {
        StartCoroutine(Winning());
    }

    public void OpenBook(int id)
    {
        bookMain.gameObject.SetActive(true);
        books[id].gameObject.SetActive(true);
    }
}
