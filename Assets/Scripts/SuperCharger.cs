using UnityEngine;
using System.Collections;

public class SuperCharger : MonoBehaviour {


    public int superDuration;

    public float doomTimer;
    public float birthTime;

    public bool flashing;
    public bool flashed;
    public float lastFlash;
    public float flashCooldown;

    public Sprite defaultSprite;
    public Sprite invisibleSprite;

    // Use this for initialization
    void Start()
    {
        birthTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= birthTime + (doomTimer * .3f))
        {
            flashing = true;
        }

        if (Time.time >= birthTime + doomTimer)
        {
            Destroy(gameObject);
        }

        /*if (flashing) {
            if (!flashed && Time.time >= lastFlash + flashCooldown) {
                GetComponent<SpriteRenderer>().sprite = defaultSprite;
                lastFlash = Time.time;
            }
            else if (Time.time >= lastFlash + flashCooldown) {
                GetComponent<SpriteRenderer>().sprite = invisibleSprite;
                lastFlash = Time.time;
            }
        }*/
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().superCharged = true;
            other.GetComponent<PlayerController>().scB = Time.time + 5;
            other.GetComponent<PlayerController>().scT = Time.time;
            Destroy(gameObject);
        }
    }
}
