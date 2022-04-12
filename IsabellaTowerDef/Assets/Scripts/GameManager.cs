using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    //public Transform enemy2Prefab;

    public float timeBetweenEnemy = 1f;
    public float countdownTimer = 3f;

    public int amountOfEnemies = 20;

    public float countdown = 3.1f;
    float setCountdown;

    public Text countdownText;
    public Button clearButton;
    bool startedSpawn = false;
    bool gameStarted = false;

    List<GameObject> listOfEnemy = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        setCountdown = countdown;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted == true)
        {
            countdown -= Time.deltaTime;
            if (countdown >= 0)
            {
                countdownText.text = Mathf.Floor(countdown).ToString();
            }
            else
            {
                countdownText.gameObject.SetActive(false);
            }
            if (countdown <= 0 && startedSpawn == false)
            {
                clearButton.gameObject.SetActive(true);
                StartCoroutine(SpawnWave());
            }
        }

    }

    public void StartGame() {
        gameStarted = true;
    }

    IEnumerator SpawnWave() {
        startedSpawn = true;
        for (int i = amountOfEnemies;  i--> 0;) {
            //Debug.Log("spawned");
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemy);
        }
    }

    void SpawnEnemy() {
        GameObject en = Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject;
        listOfEnemy.Add(en);
    }

    public void Clear()
    {
        gameStarted = false;
        startedSpawn = false;
        countdownText.gameObject.SetActive(true);
        countdown = setCountdown;
        clearButton.gameObject.SetActive(false);
        foreach (GameObject go in listOfEnemy) {
            Destroy(go);
        }
        StopAllCoroutines();
    }

 
}
