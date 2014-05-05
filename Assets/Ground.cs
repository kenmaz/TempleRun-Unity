using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	private GameObject groundControl;

	// Use this for initialization
	void Start () {
		this.groundControl = GameObject.Find("GroundControl");
	}
	
	// Update is called once per frame
	void Update () {

		//if out of camera, recycle this ground.
		Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position);	
		if (pos.z < 0) {
			GroundControl con = groundControl.GetComponent<GroundControl>();
			con.recycle();
		}
	}

}
