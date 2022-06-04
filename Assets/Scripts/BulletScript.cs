using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    public float speed;
    public int damage;
    public float ttl;

    private float birthTime;

	// Use this for initialization
	void Start () {
        birthTime = Time.time;
        if (ttl == 0.0f){ 
            ttl = 10.0f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= birthTime + ttl){
            Destroy(gameObject);
        }
        transform.Translate(Vector3.up * speed);
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "enemy") {
            other.GetComponent<EnemyAI>().health -= damage;
            other.GetComponent<EnemyAI>().lastTimeHit = Time.time;
        }
        if (other.tag != "terrain"){
            Destroy(gameObject);
        }
    }
}
