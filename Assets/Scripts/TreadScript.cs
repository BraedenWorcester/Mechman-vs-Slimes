using UnityEngine;
using System.Collections;

public class TreadScript : MonoBehaviour {

    public bool movingBackward;
    public bool movingForward;

    public bool movingLeft;
    public bool movingRight;

    public Vector3 rotation;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.D)) {
            movingRight = true;
        }
        else if (Input.GetKey(KeyCode.A)) {
            movingLeft = true;
        }
        if (Input.GetKey(KeyCode.W)) {
            movingForward = true;
        }
        else if (Input.GetKey(KeyCode.S)) {

            movingBackward = true;
        }

        if (!Input.GetKey(KeyCode.D)) {
            movingRight = false;
        }
        if (!Input.GetKey(KeyCode.A)) {
            movingLeft = false;
        }
        if (!Input.GetKey(KeyCode.W)) {
            movingForward = false;
        }
        if (!Input.GetKey(KeyCode.S)) {
            movingBackward = false;
        }

        
    }

    void LateUpdate() {
        if (movingForward && movingRight) {
            rotation = new Vector3(0, 0, -45);
        }
        else if (movingForward && movingLeft) {
            rotation = new Vector3(0, 0, 45);
        }
        else if (movingForward) {
            rotation = new Vector3(0, 0, 0);
        }
        else if (movingBackward && movingRight) {
            rotation = new Vector3(0, 0, -120);
        }
        else if (movingBackward && movingLeft) {
            rotation = new Vector3(0, 0, 120);
        }
        else if (movingLeft) {
            rotation = new Vector3(0, 0, 90);
        }
        else if (movingRight) {
            rotation = new Vector3(0, 0, -90);
        }
        else if (movingBackward) {
          rotation = new Vector3(0, 0, 180);
        }
        transform.eulerAngles = rotation;
        
    }
}
