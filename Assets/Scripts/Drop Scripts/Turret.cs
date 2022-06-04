using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    public float doomTimer;
    public float birthTime;

    // Use this for initialization
    void Start()
    {
        birthTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= birthTime + doomTimer)
        {
            Destroy(gameObject);
        }
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController p = other.GetComponent<PlayerController>();
            if (p.hasTurret)
            {
                //hi
            }
            else
            {
                Destroy(gameObject);
                p.hasTurret = true;
            }
        }
    }
}
