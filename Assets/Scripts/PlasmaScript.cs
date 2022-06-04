using UnityEngine;
using System.Collections;

public class PlasmaScript : MonoBehaviour {

    public float speed;
    public int damage;

    public float birth;
    public float doomTimer;

    public float lastSpeedChange;
    public float speedChange;

    // Use this for initialization
    void Start()
    {
        birth = Time.time;
        lastSpeedChange = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = (transform.up * speed);
        if (Time.time >= birth + doomTimer) {
            Destroy(gameObject);
        }

        if (Time.time >= lastSpeedChange + speedChange && speed > 0) {
            speed -= .1f;
            lastSpeedChange = Time.time;
        }
        if (speed < 0) {
            speed = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            //speed = 0;
           //other.gameObject.GetComponent<EnemyAI>().health -= damage;
           //other.gameObject.GetComponent<EnemyAI>().lastTimeHit = Time.time;
        }
        //Destroy(gameObject);
    }
}
