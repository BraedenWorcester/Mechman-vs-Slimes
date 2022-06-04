using UnityEngine;
using System.Collections;

public class BeamScript : MonoBehaviour {

    public PlayerController pC;
    public int damage;

    public float hitCoolDown;

	void Start () {
	    pC = GameObject.Find("player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "enemy") {
            if (Time.time >= hitCoolDown + other.GetComponent<EnemyAI>().lastTimeHit) {
                other.GetComponent<EnemyAI>().health -= damage;
                other.GetComponent<EnemyAI>().lastTimeHit = Time.time;
                //pC.activeAmmo--;
            }
        }
    }
}
