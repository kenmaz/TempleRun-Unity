using UnityEngine;
using System.Collections.Generic;

enum Route: int
{
	Unknown = 0,
	Straight = 1,
	Left = 2,
	Right = 3
}

public class GroundControl : MonoBehaviour
{
	private List<GameObject> defaultGroundList;
	private List<Vector3> defaultGroundPosList;

	private LinkedList<GameObject> grounds;
	private Route next;

	// Use this for initialization
	void Start()
	{
		Application.targetFrameRate = 60;

		this.defaultGroundList = new List<GameObject>();
		this.defaultGroundPosList = new List<Vector3>();
		this.grounds = new LinkedList<GameObject>();

		for (int i = 1; i <= 4; i++) {
			GameObject ground = GameObject.Find("Ground" + i);
			defaultGroundList.Add(ground);
			defaultGroundPosList.Add(ground.transform.position);
			grounds.AddLast(ground);
		}
	}

	public void resetGame()
	{
		for (int i = 0; i < this.defaultGroundList.Count; i++) {
			GameObject ground = this.defaultGroundList[i];
			ground.transform.position = this.defaultGroundPosList[i];
			ground.transform.rotation = Quaternion.identity;
		}
		Start();
	}

	// Update is called once per frame
	void Update()
	{
	}

	public void recycle()
	{
		GameObject first = this.grounds.First.Value;
		GameObject last = this.grounds.Last.Value;
		this.grounds.Remove(first);
		this.grounds.AddLast(first);
		Debug.Log(first);
		
		first.transform.rotation = last.transform.rotation;
		
		float rotateAdjust = first.transform.localScale.z / 2 - first.transform.localScale.x / 2;

		if (next == Route.Unknown) {
			next = (Route)Random.Range(1, 4);
		} 

		switch (next) {
			case Route.Straight:
				first.transform.position = last.transform.position;
				first.transform.Translate(0, 0, last.transform.localScale.z);
				next = Route.Unknown;
				break;
			case Route.Left:
				first.transform.position = last.transform.position;
				first.transform.Rotate(Vector3.up, -90.0f);
				first.transform.Translate(rotateAdjust, 0, rotateAdjust);
				next = Route.Straight;
				break;
			case Route.Right:
				first.transform.position = last.transform.position;
				first.transform.Rotate(Vector3.up, 90.0f);
				first.transform.Translate(-1 * rotateAdjust, 0, rotateAdjust);
				next = Route.Straight;
				break;
			default:
				break;
		}
	}

}
