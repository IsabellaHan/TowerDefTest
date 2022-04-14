using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject enemyPrefab;
    public GameObject enemyPrefabTwo;

    public float timeBetweenEnemy = 1f;
    public float countdownTimer = 3f;

    public int amountOfEnemies = 20;
    public int enemyCounter = 0;

    public float countdown = 3.1f;
    float waveTwoCountdown = 3.1f;
    float setCountdown;

    public int lives = 5;
    public int score = 0;

    public Text countdownText;
    public Text scoreText;
    public Button[] allButtons;
    bool startedSpawn = false;
    bool gameStarted = false;

    public List<GameObject> listOfEnemy = new List<GameObject>();
    public List<GameObject> listOfTurrets = new List<GameObject>();
    public Node node;

    AudioSource audio;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        setCountdown = countdown;
        scoreText.text = "Lives: "+lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted == true)
        {
            if (enemyCounter < amountOfEnemies) // start first wave
            {
                countdown -= Time.deltaTime;
                if (countdown >= 0)
                {
                    countdownText.text = "Wave 1 starting in: \n" +Mathf.Floor(countdown).ToString();
                }
                else
                {
                    countdownText.text = "  ";
                }
                if (countdown <= 0 && startedSpawn == false)
                {
                    foreach (Button b in allButtons)
                    {
                        b.gameObject.SetActive(true);
                    }
                    StartCoroutine(SpawnWave(enemyPrefab));
                }
            }

            else // start second wave
            {
                waveTwoCountdown -= Time.deltaTime;
                //StopCoroutine(SpawnWave(enemyPrefab));
                if (waveTwoCountdown >= 0)
                {
                    countdownText.text = "Wave 2 starting in: \n" + Mathf.Floor(waveTwoCountdown).ToString();
                    startedSpawn = false;
                }
                else {
                    countdownText.text = "  ";
                }
                if (waveTwoCountdown <= 0 && startedSpawn == false)
                {
                    StartCoroutine(SpawnWave(enemyPrefabTwo));
                }
            }
        }

        scoreText.text = "Lives: " + lives.ToString();
        if (lives <= 0) {
            scoreText.text = "Lives: " + lives.ToString() + "\n  You LOSE!";
            StopAllCoroutines();
            Time.timeScale = 0;
        }

        if (enemyCounter >= 40 && listOfEnemy.Count == 0) {
            scoreText.text = "You win!";
        }
        
    }

    public void StartGame() {
        gameStarted = true;
    }

 


    IEnumerator SpawnWave(GameObject enemytype) {
        startedSpawn = true;
        for (int i = amountOfEnemies;  i--> 0;) {
            SpawnEnemy(enemytype);
            yield return new WaitForSeconds(timeBetweenEnemy);
        }
    }

    void SpawnEnemy(GameObject enemytype) {
        GameObject en = Instantiate(enemytype, transform.position, transform.rotation) as GameObject;
        listOfEnemy.Add(en);
    }

    public void Clear()
    {
       
        SceneManager.LoadScene("TestLevel");
    }

    public void BuildTurret(GameObject tur) {
        GameObject t = Instantiate(tur, node.transform.position, node.transform.rotation);
        listOfTurrets.Add(t);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
