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
		Vector2 userPos = new Vector2(user.transform.position.x,
		                              user.transform.position.z);

		GameObject ground = this.grounds.First.Value;

		//z-y check
		Rect groundRect;
		float angle = ground.transform.rotation.eulerAngles.y;
		if (angle == 0 || angle == 180) {
			float z = ground.transform.position.z;
			float x = ground.transform.position.x;
			float width = ground.transform.localScale.x;
			float height = ground.transform.localScale.z; //user and ground.edge offset
			groundRect = new Rect(x - (width / 2),
			                      z + (height / 2),
			                      width,
			                      height);
		} else {
			float z = ground.transform.position.z;
			float x = ground.transform.position.x;
			float width = ground.transform.localScale.z;
			float height = ground.transform.localScale.x; //user and ground.edge offset
			groundRect = new Rect(x - (width / 2),
			                      z + (height / 2),
			                      width,
			                      height);
		}


		//Debug.Log(string.Format("{0} {1} {2}", ground.name, groundRect, userPos));

		//on ground ?
		if (groundRect.Contains(userPos)) {
			Debug.Log(string.Format("out! {0}", ground.name));
			Debug.Log(string.Format("{0} {1} {2} y:{3}", ground.name, groundRect, userPos, ground.transform.rotation.eulerAngles));
			this.recycleGround();
		}
	}

	private void recycleGround()
	{
		GameObject tailGround = this.grounds.Last.Value;

		LinkedListNode<GameObject> targetNode = this.grounds.First;
		this.grounds.RemoveFirst();
		this.grounds.AddLast(targetNode);

		GameObject targetGround = targetNode.Value;
		targetGround.transform.rotation = tailGround.transform.rotation;

		// add straight
		Vector3 pos = tailGround.transform.position;
		pos.z += tailGround.transform.localScale.z;
		targetGround.transform.position = pos;

		Vector3 rotatePoint = new Vector3(pos.x,
		                                  pos.y,
		                                  pos.z - (targetGround.transform.localScale.z / 2) + (targetGround.transform.localScale.x / 2));
		targetGround.transform.RotateAround(rotatePoint, Vector3.up, 90);


	}
}
