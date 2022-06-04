using UnityEngine;
using System.Collections;

public class TurretAI : MonoBehaviour {

    public GameController gc;
    public PlayerController pc;
    public GameObject player;
    public GameObject target;
    public GameObject stand;
    public float fireRate;
    public float lastTimeFired;
    public GameObject projectile; //prefab for instantiating bullet
    public Transform shotSpawn;
    public int ammo;
    public bool superCharged;

	// Use this for initialization
	void Start () {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        player = GameObject.Find("player");
        pc = player.GetComponent<PlayerController>();
        superCharged = pc.superCharged;
        if(superCharged)
        {
            fireRate = 0.08f;
            ammo *= 2;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null) {
            DetermineTarget();
        }
        else {
            if (gc.livingEnemies.Count > 0)
                target = gc.livingEnemies[0];
        }

        if (target != null) {
            Vector3 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            if (Time.time >= lastTimeFired + fireRate) {
                Instantiate(projectile, shotSpawn.position, transform.rotation);
                lastTimeFired = Time.time;
                ammo--;
            }
        }

        if (ammo <= 0) {
            Destroy(stand);
            Destroy(gameObject);
        }
        
    }

    public void DetermineTarget() {
        float closestDistance = 0;
        GameObject closest = null;
        for (int i = 0; i < gc.livingEnemies.Count; i++) {
            if (Vector2.Distance(gc.livingEnemies[i].transform.position, player.transform.position) < closestDistance || closestDistance == 0) {
                closest = gc.livingEnemies[i];
                closestDistance = Vector2.Distance(gc.livingEnemies[i].transform.position, player.transform.position);
            }
        }
        target = closest;
    }
}
