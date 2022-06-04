using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HardCoreButtonScript : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (PlayerPrefs.GetInt("hardcore", -1) == -1 || PlayerPrefs.GetInt("hardcore", -1) == 0) {
            text.text = "Turn On Hardcore Mode";
        }
        else {
            text.text = "Turn Off Hardcore Mode";
        }
	}
}
