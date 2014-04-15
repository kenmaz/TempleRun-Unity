using UnityEngine;
using System.Collections;

public class User : MonoBehaviour
{
	public float defaultRunSpeed = 7.0f;
	public float speedUpRate = 1.0f;
	public GUIText elapseTimeGUIText;
	private float runSpeed;
	private	int runDirection;


	// Use this for initialization
	void Start()
	{
		this.runSpeed = this.defaultRunSpeed;
	}
	
	// Update is called once per frame
	void Update()
	{
		this.processKeyInput();
		this.processTouchInput();

		this.move();
		this.updateElapsedTimeLabel();
		this.speedUp();

		this.checkFail ();
	}

	// touch
	private Vector2 touchStartPos;

	private void processTouchInput()
	{
		if (Input.touchCount == 0) {
			return;
		}

		Touch touch = Input.touches [0];
		TouchPhase phase = touch.phase;
		if (phase == TouchPhase.Began) {
			touchStartPos = touch.position;
		} else if (phase == TouchPhase.Ended || phase == TouchPhase.Canceled) {
			if (touch.position.x - touchStartPos.x < 0) {
				runDirection -= 90;
			} else {
				runDirection += 90;
			}
		}
	}

	//keyborad
	private float lastRotateTime;
	private bool  arrowKeyPressed;

	private void processKeyInput()
	{
		if (!arrowKeyPressed && Time.time - lastRotateTime > 0.1f) {
			if (Input.GetKey(KeyCode.LeftArrow)) { 
				runDirection -= 90;  
				lastRotateTime = Time.time;
				arrowKeyPressed = true;
				
			} else if (Input.GetKey(KeyCode.RightArrow)) {
				runDirection += 90;
				lastRotateTime = Time.time;
				arrowKeyPressed = true;
			}
		}
		
		if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) {
			arrowKeyPressed = false;
		}
	}

	private void move()
	{
		this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, runDirection, 0), 0.25f);

		Vector3 v = transform.forward * this.runSpeed;
		v.y = this.rigidbody.velocity.y;
		this.rigidbody.velocity = v;
	}

	private void speedUp()
	{
		//speed up 0.1 per 10sec.
		this.runSpeed = defaultRunSpeed + Time.time / 10.0f * this.speedUpRate;
	}

	private void updateElapsedTimeLabel()
	{
		float now = Time.time;
		int sec = (int)now;
		float mil = (now - (float)sec) * 100.0f;
		this.elapseTimeGUIText.text = string.Format("{0:00}:{1:00}", sec, mil);
	}

	private void checkFail() {
		if (transform.position.y < -10) {
			transform.position = new Vector3(0, 5, 0);
			transform.rotation = Quaternion.identity;
			this.runDirection = 0;
			this.runSpeed = this.defaultRunSpeed;
		}
	}
}
