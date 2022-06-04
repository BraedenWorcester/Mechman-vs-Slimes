using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponSlotUIScript : MonoBehaviour {

    public GameObject player;
    public int type;
    private GameObject child;

    public Sprite on;
    public Sprite off;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("player");
        child = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null) {
            if (player.GetComponent<PlayerController>().gunType == type) {
                child.GetComponent<Image>().sprite = on;
            }
            else {
                child.GetComponent<Image>().sprite = off;
            }
        }
	}
}
