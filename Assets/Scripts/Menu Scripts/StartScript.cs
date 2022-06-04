using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

    public Button btn;
    public string missionName;

	// Use this for initialization
	void Start () {
        btn = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    public void LoadMission(string mission) {
        SceneManager.LoadScene(mission);
    }
}
