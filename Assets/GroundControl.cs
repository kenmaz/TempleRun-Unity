using UnityEngine;
using System.Collections.Generic;

public class GroundControl : MonoBehaviour
{

	public GameObject ground1;
	public GameObject ground2;
	public GameObject ground3;
	public GameObject user;
	private LinkedList<GameObject> grounds;
	private Vector3 orgPos1, orgPos2, orgPos3;

	// Use this for initialization
	void Start()
	{
		this.grounds = new LinkedList<GameObject>();
		this.grounds.AddLast(this.ground1);
		this.grounds.AddLast(this.ground2);
		this.grounds.AddLast(this.ground3);
		this.orgPos1 = this.ground1.transform.position;
		this.orgPos2 = this.ground2.transform.position;
		this.orgPos3 = this.ground3.transform.position;
	}

	public void resetGame() {
		this.ground1.transform.position = this.orgPos1;
		this.ground1.transform.rotation = Quaternion.identity;
		this.ground2.transform.position = this.orgPos2;
		this.ground2.transform.rotation = Quaternion.identity;
		this.ground3.transform.position = this.orgPos3;
		this.ground3.transform.rotation = Quaternion.identity;
		this.grounds = new LinkedList<GameObject>();
		Start();
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void recycle(GameObject targetGround) {
		Debug.Log(string.Format("recycle:{0}", targetGround));

		GameObject tailGround = this.grounds.Last.Value;

		this.grounds.Remove(targetGround);
		this.grounds.AddLast(targetGround);

		targetGround.transform.rotation = tailGround.transform.rotation;
		
		// add straigh
		Vector3 pos = tailGround.transform.position;
		pos.z += tailGround.transform.localScale.z;
		targetGround.transform.position = pos;
		
		Vector3 rotatePoint = new Vector3(pos.x,
		                                  pos.y,
		                                  pos.z - (targetGround.transform.localScale.z / 2) + (targetGround.transform.localScale.x / 2));
		targetGround.transform.RotateAround(rotatePoint, Vector3.up, 90);
	}

}
