using UnityEngine;
using System.Collections;

public class SuperAmmo : MonoBehaviour {

    public float speed;
    public int damage;
    public int gunType;
    public float birth;
    public float doomTimer;
    public float lastSpeedChange;
    public float speedChange;

    private bool hypno;
    private bool hypnoTouch;
    public float hypnoCoolDown;
    public float lastHypno;

    public Sprite hypnoSprite;
    public Sprite lessHypnoSprite;

    public Sprite[] bullat;

    private PlayerController player;
    private SpriteRenderer bullet;
    private Collider2D col;
    private CircleCollider2D ccol;
    
    void Start () {
        player = GameObject.Find("player").GetComponent<PlayerController>();
        gunType = player.gunType;
        bullet = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        if (gunType == 1)
        {

        }
        else if (gunType == 2)
        {
            bullet.sprite = bullat[0];
            damage = 35;
            speed = .15f;
            birth = Time.time;
            doomTimer = 1.5f;
        }
        else if(gunType == 4)
        {
            bullet.sprite = bullat[2];
            damage = 0;
            speed = .05f;
            doomTimer = 2f;
            birth = Time.time;
        }
        else if(gunType == 5)
        {
            bullet.sprite = bullat[4];
            damage = 0;
            transform.position = player.transform.position;
            doomTimer = 1000000f;
            birth = Time.time;
            col.isTrigger = false;
            ccol = gameObject.AddComponent<CircleCollider2D>();
            ccol.radius = 0.5f;
        }
	}
	
	void Update () {
        if(gunType == 51)
        {
            GetComponent<Rigidbody2D>().velocity = (transform.up * speed);
            if (Time.time >= birth + doomTimer)
            {
                Destroy(gameObject);
            }

            if (Time.time >= lastSpeedChange + speedChange && speed > 0)
            {
                speed -= .1f;
                lastSpeedChange = Time.time;
            }
            if (speed < 0)
            {
                speed = 0;
            }
        }
        else if(gunType == 2 || gunType == 4)
        {
            if (Time.time >= birth + doomTimer)
            {
                Destroy(gameObject);
            }

            if (!hypnoTouch)
            {
                transform.Translate(Vector3.up * speed);
            }

            if(gunType == 4)
            {
                if (Time.time >= lastHypno + hypnoCoolDown)
                {
                    if (!hypno)
                    {
                        GetComponent<SpriteRenderer>().sprite = hypnoSprite;
                        hypno = true;

                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().sprite = lessHypnoSprite;
                        hypno = false;
                    }
                    lastHypno = Time.time;
                }
            }
        }
        if(gunType == 5)
        {
            transform.position = player.transform.position;
            transform.localScale = new Vector3(3, 3, 3);
            if (Input.GetMouseButtonUp(1))
            {
                Destroy(gameObject);
                player.pulsed = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            other.GetComponent<EnemyAI>().health -= damage;
            other.GetComponent<EnemyAI>().lastTimeHit = Time.time;
            if(gunType == 4)
            {
                other.GetComponent<EnemyAI>().hypnotized = true;
                other.GetComponent<EnemyAI>().hypnoStartTime = Time.time;
                transform.localScale = new Vector3(3, 3, 3);
                hypnoTouch = true;
            }
        }
        if(gunType != 2 && gunType != 4 && gunType != 5)
        {
            Destroy(gameObject);
        }
    }
}
