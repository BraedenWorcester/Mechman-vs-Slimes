using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HypnoBulletScript : MonoBehaviour {

    public float speed;

    public float doc;
    public float doomTimer;

    private bool hypno;
    public float hypnoCoolDown;
    public float lastHypno;

    public Sprite hypnoSprite;
    public Sprite lessHypnoSprite;

    public float timeToIncrease;
    public float lastIncreaseTime;
    public float increaseScale;

	// Use this for initialization
	void Start () {
        doc = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * speed);
        if (Time.time >= lastHypno + hypnoCoolDown) {
            if (!hypno) {
                GetComponent<SpriteRenderer>().sprite = hypnoSprite;
                hypno = true;
                
            }
            else {
                GetComponent<SpriteRenderer>().sprite = lessHypnoSprite;
                hypno = false;
            }
            lastHypno = Time.time;
        }

        if (Time.time >= lastIncreaseTime + timeToIncrease){
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * increaseScale, gameObject.transform.localScale.y * increaseScale, 1);
            lastIncreaseTime = Time.time;
        }

        if (Time.time >= doc + doomTimer) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "enemy") {
            other.GetComponent<EnemyAI>().hypnotized = true;
            other.GetComponent<EnemyAI>().hypnoStartTime = Time.time;
        }

    }
}
