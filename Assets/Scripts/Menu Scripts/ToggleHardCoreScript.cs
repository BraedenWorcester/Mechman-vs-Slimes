using UnityEngine;
using System.Collections;

public class ToggleHardCoreScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ToggleHardCoreMode() {
        if (PlayerPrefs.GetInt("hardcore", -1) == -1 || PlayerPrefs.GetInt("hardcore", -1) == 0) {
            PlayerPrefs.SetInt("hardcore", 1);
        }
        else {
            PlayerPrefs.SetInt("hardcore", 0);
        }
        Debug.Log("" + PlayerPrefs.GetInt("hardcore", -1));
    }
}
