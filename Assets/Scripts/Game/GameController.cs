using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour
{
    public GameObject[] enemies;
    public int totalEnemies = 0;
    public static GameController instance = null;

    GameObject[] enemySpawnPoints;
    Slider playerHealthSlider;
    Slider statueHealthSlider;

    readonly int maxEnemies = 10;
    int playerHealth = 100;
    int statueHealth = 100;
    float spawnTime = 1f;

    private void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.Find("InputManager"));

        GetComponent<CanvasController>().enabled = false;
        GetComponent<Compiler>().enabled = false;
    }

    private void Start() {
        int y = 200;
        int buttonNumber = 1;
        GameObject button;
        GameObject canvas = GameObject.Find("Canvas");
        List<string> buttonList = GetComponent<XmlReader>().ReadXml("Level List.xml", "level", "name");

        foreach (string element in buttonList) {
            button = Instantiate(Resources.Load<GameObject>("Prefabs/Menu/Menu Button"), new Vector3(0, y, 0), Quaternion.identity);
            y -= 90;

            /* É preciso criar uma variável temporária pro lambda funcionar, senão ele sempre vai gravar o valor do último valor passado (não faz muito sentido) */
            int tempI = buttonNumber;
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(tempI));

            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = element;
            button.transform.SetParent(canvas.transform, false);
            buttonNumber++;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update() {
        if (SceneManager.GetActiveScene().name != "Main Menu") {
            if (Luminosity.IO.InputManager.GetButtonDown("Interact", 0)) {
                //StartGame();
            }
        }
    }

    public void StartGame() {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn() {
        if(totalEnemies < maxEnemies) {
            Instantiate(enemies[UnityEngine.Random.Range(0, enemies.Length)], enemySpawnPoints[UnityEngine.Random.Range(0, enemySpawnPoints.Length)].transform.position, Quaternion.identity);
            totalEnemies++;
        }
    }

    public void DamagePlayerHealth(int damage) {
        playerHealth -= damage;
        if (playerHealth <= 0) GameOver("Player");
            playerHealthSlider.value = playerHealth;
    }

    public void DamageStatueHealth(int damage) {
        statueHealth -= damage;
        if (statueHealth <= 0) GameOver("Statue");
            statueHealthSlider.value = statueHealth;
    }

    void GameOver(string target) {
        Time.timeScale = 0;
    }

    public void LoadLevel(int levelID) {
        switch (levelID) {
            case 1:
                SceneManager.LoadScene("City Square", LoadSceneMode.Single);
                SceneManager.LoadScene("Tutorial Script", LoadSceneMode.Additive);
                instance.GetComponent<TutorialController>().SetTutorial("Level_1.xml");
                break;

            default:
                SceneManager.LoadScene("City Square", LoadSceneMode.Single);
                break;
        }

        SceneManager.LoadScene("canvas", LoadSceneMode.Additive);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if(scene.name == "canvas" && instance != null) {
            playerHealthSlider = GameObject.Find("Player Health Slider").GetComponent<Slider>();
            statueHealthSlider = GameObject.Find("Statue Health Slider").GetComponent<Slider>();
            instance.GetComponent<Compiler>().enabled = true;
            instance.GetComponent<CanvasController>().enabled = true;
            enemySpawnPoints = GameObject.FindGameObjectsWithTag("Enemy Spawn");
        }
    }
}
