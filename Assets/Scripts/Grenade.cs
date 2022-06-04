using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour
{

    public float speed;
    public int damage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            other.GetComponent<EnemyAI>().health -= damage;
            other.GetComponent<EnemyAI>().lastTimeHit = Time.time;
        }
        
    }
}
