using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public GameController gc;
    private PlayerController pc;

    public float speed;
    public int damage;

    public GameObject player;

    public int health;
    public float lastTimeHit;
    public float hitDisappearTime;

    public bool waveStunned;
    public float lastWaveStunTime;
    public float timeToRecoverWaveStun;

    public bool hypnotized;
    public float hypnoFlashTime;
    public float lastHypnoFlashTime;
    public bool yellow;
    public float hypnoStartTime;
    public float hypnoTimer;

    public int dropChance; // measured as one out of dropChance. So if this equals 4, then it's 1/4 chance to drop

	void Start () {
        player = GameObject.Find("player");
        pc = player.GetComponent<PlayerController>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        gc.livingEnemies.Add(gameObject);
        if (gc.hardcore == 2)
            damage = 9999999;
	}
	
	// Update is called once per frame
	void Update () {
        if (waveStunned && Time.time >= lastWaveStunTime + timeToRecoverWaveStun){
            waveStunned = false;
        }
        if (player != null && !waveStunned) {
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
        if (health > 0) {
            if (!hypnotized && !waveStunned) {
                GetComponent<SpriteRenderer>().flipY = false;
                GetComponent<Rigidbody2D>().velocity = (transform.up * speed * gc.hardcore);
            }
            else {
                if (!waveStunned){
                    GetComponent<Rigidbody2D>().velocity = (transform.up * -speed);
                    GetComponent<SpriteRenderer>().flipY = true;
                }
                if (Time.time >= hypnoFlashTime + lastHypnoFlashTime) {
                    //Debug.Log("hypnononononononononononno");
                    if (yellow) {
                        GetComponent<SpriteRenderer>().color = Color.white;
                        yellow = false;
                        lastHypnoFlashTime = Time.time;
                    }
                    else {
                        GetComponent<SpriteRenderer>().color = Color.yellow;
                        yellow = true;
                        lastHypnoFlashTime = Time.time;
                    }
                }
            }
        }

        if (Time.time >= hypnoStartTime + hypnoTimer) {
            hypnotized = false;
        }

        if (health <= 0 && Time.time >= lastTimeHit + hitDisappearTime) {
            if (player.GetComponent<PlayerController>().gunType == 6) {
                if (Random.Range(0, dropChance / 2) == 1) {
                    Instantiate(gc.itemDrops[Random.Range(0, gc.itemDrops.Count)], transform.position, Quaternion.identity);
                }
            }
            else {
                if (Random.Range(0, dropChance) == 1) {
                    Instantiate(gc.itemDrops[Random.Range(0, gc.itemDrops.Count)], transform.position, Quaternion.identity);
                }
            }
            if (Random.Range(0, dropChance) == 1) {
                Instantiate(gc.itemDrops[Random.Range(0, gc.itemDrops.Count)], transform.position, Quaternion.identity);
            }
            KillMe();
        }
        if (Time.time <= lastTimeHit + hitDisappearTime) {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (!hypnotized){
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (!pc.superCharged && health > 0)
            {
                other.gameObject.GetComponent<PlayerController>().health -= damage;
                other.gameObject.GetComponent<PlayerController>().lastTimeHit = Time.time;
                KillMe();
            }
        }
    }
    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "wave") {
            lastWaveStunTime = Time.time;
            waveStunned = true;
        }
    }
    public void KillMe() {
        gc.livingEnemies.Remove(gameObject);
        Destroy(gameObject);
    }
}
