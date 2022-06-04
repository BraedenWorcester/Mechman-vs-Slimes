using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToOptionsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void GoToOptions() {
        SceneManager.LoadScene("OptionsMenu");
    }
}
