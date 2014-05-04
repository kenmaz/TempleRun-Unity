using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

	public GameObject groundControl;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//if out of camera, recycle this ground.
		Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position);	
		if (pos.z < 0) {
			GroundControl con = groundControl.GetComponent<GroundControl>();
			con.recycle(this.gameObject);
		}
	}

}
