using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour
{

	private LinkedList<GameObject> grounds;

	// Use this for initialization
	void Start()
	{
		this.grounds = new LinkedList<GameObject>();
		this.grounds.AddLast(GameObject.Find("Ground1"));
		this.grounds.AddLast(GameObject.Find("Ground2"));
		this.grounds.AddLast(GameObject.Find("Ground3"));
	}
	
	// Update is called once per frame
	void Update()
	{
		return;
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			Debug.Log("up");
			this.addUp(KeyCode.UpArrow);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			Debug.Log("left");
			this.addUp(KeyCode.LeftArrow);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			Debug.Log("right");
			this.addUp(KeyCode.RightArrow);
		}
	}

	void addUp(KeyCode keycode)
	{
		GameObject first = this.grounds.First.Value;
		GameObject last = this.grounds.Last.Value;
		this.grounds.Remove(first);
		this.grounds.AddLast(first);
		Debug.Log(first);

		first.transform.rotation = last.transform.rotation;

		float rotateAdjust = first.transform.localScale.z / 2 - first.transform.localScale.x / 2;

		switch (keycode) {
			case KeyCode.UpArrow:
				first.transform.position = last.transform.position;
				first.transform.Translate(0, 0, last.transform.localScale.z);
				break;
			case KeyCode.LeftArrow:
				first.transform.position = last.transform.position;
				first.transform.Rotate(Vector3.up, -90.0f);
				first.transform.Translate(rotateAdjust, 0, rotateAdjust);
				break;
			case KeyCode.RightArrow:
				first.transform.position = last.transform.position;
				first.transform.Rotate(Vector3.up, 90.0f);
				first.transform.Translate(-1 * rotateAdjust, 0, rotateAdjust);
				break;
			default:
				break;
		}

	}
}
