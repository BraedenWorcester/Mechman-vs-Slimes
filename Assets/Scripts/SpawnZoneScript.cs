using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnZoneScript : MonoBehaviour {

    public GameController gc;

    public float size;

    public List<GameObject> possibleSpawns = new List<GameObject>();
    public float spawnDelay;
    public float lastSpawnTime;

	// Use this for initialization
	void Start () {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (gc.inWave && gc.enemiesNeededToSpawn != 0 && Time.time >= lastSpawnTime + spawnDelay) {
            Instantiate(possibleSpawns[Random.Range(0, possibleSpawns.Count)], new Vector2(transform.position.x + Random.Range(-size, size), transform.position.y + Random.Range(-size, size)), transform.rotation);
            gc.enemiesNeededToSpawn -= 1;
            lastSpawnTime = Time.time;
        }
	}
}
