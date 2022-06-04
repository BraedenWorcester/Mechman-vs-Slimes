using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    #region DebugBools
    public bool debugMode;
    public bool everything;
    public bool infiniteAmmo;

    #endregion

    public int hardcore;

    public GameObject player;

    public List<GameObject> itemDrops = new List<GameObject>();
    public List<GameObject> enemyPrefabs = new List<GameObject>();
    public GameObject canvasWeapons;
    public GameObject canvasHP;
    public Text waveText;
    public List<GameObject> livingEnemies = new List<GameObject>();

    public GameObject floor;

    public bool ValidateLocation(Vector2 location){
        return floor.GetComponent<BoxCollider2D>().bounds.Contains(location);
    }

    public int wave;
    public bool inWave;
    public float waveDelay;
    public float lastWaveEndTime;
    public int enemiesPerWave;
    public int enemiesNeededToSpawn;

    public float endTimer;
    public float deathTime;
	void Start () {
        if (PlayerPrefs.GetInt("hardcore", -1) == -1 || PlayerPrefs.GetInt("hardcore", -1) == 0) {
            hardcore = 1;
        }
        else {
            hardcore = 2;
        }
        player = GameObject.Find("player");
        enemiesPerWave = 2;
        wave = 0;
        Physics2D.IgnoreLayerCollision(10, 0, true);
        Physics2D.IgnoreLayerCollision(9, 0, true);
	}
	
	// Update is called once per frame
	void Update () {
        
        waveText.text = "" + wave;
	    if (livingEnemies.Count == 0 && enemiesNeededToSpawn == 0) {
            ChangeWave();
            inWave = false;
        }
        if (Time.time >= lastWaveEndTime + waveDelay && !inWave) {
            wave++;
            inWave = true;
        }
        if (Input.GetKeyDown("`") && !debugMode)
        {
            canvasWeapons.GetComponent<Canvas>().enabled = false;
            canvasHP.GetComponent<Canvas>().enabled = false;
            debugMode = true;
        }
        else if(Input.GetKeyDown("`") && debugMode)
        {
            canvasWeapons.GetComponent<Canvas>().enabled = true;
            canvasHP.GetComponent<Canvas>().enabled = true;
            debugMode = false;
        }

        if (player == null && deathTime == 0)
            deathTime = Time.time;
        else if (player == null && Time.time >= deathTime + endTimer)
            SceneManager.LoadScene("MainMenu");
    }

    private void ChangeWave() {
        lastWaveEndTime = Time.time;
        if (enemiesPerWave < 50) {
            float temp = enemiesPerWave * 2;
            enemiesPerWave = (int)temp;
        }
        else {
            enemiesPerWave += 10;
        }
        enemiesNeededToSpawn = enemiesPerWave;
    }
}
