using UnityEngine;
using System;
using System.Collections;
using BladeCast;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public GameObject bigBoom;
	public GameObject spark;
	public GameObject boostCloud;
	public float boostSpeed;
	public float boostDur; //Boost Durraction
	public AudioClip death;
	public AudioClip bump;
	public AudioClip boostSound;
	public int lives;
	public int cid;

	private Rigidbody rb;
	private AudioSource source;
	private int boostCounter;
	private bool boosting;
	private Vector3 originSize;
	private int inactiveTime;

	void Start () {
		//BCMessenger.Instance.RegisterListener ("
		BCMessenger.Instance.RegisterListener ("boost", 0, this.gameObject, "StartBoost");
		BCMessenger.Instance.RegisterListener ("gyro" , 0, this.gameObject, "MovePlayer");
		rb = GetComponent<Rigidbody>();	
		boostCounter = 0;
		boosting = false;
		originSize = this.transform.localScale;
		source = GetComponent<AudioSource> ();
		boostSpeed = 20;
		lives = 5;
	}

	void FixedUpdate () {
		boostCounter++;
		inactiveTime++;
		print (inactiveTime);
		if (inactiveTime > 500) {
			GameObject temp = (GameObject)Instantiate(bigBoom, this.transform.position, Quaternion.identity);
			source.PlayOneShot(death, 0.5f);
			if(this.gameObject)
			{
				//Destroy(this.gameObject);
				this.gameObject.SetActive (false);
			}
			//this.gameObject.SetActive(false);
		}
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		bool boost = Input.GetKeyUp ("space");

		if (boosting && boostCounter > boostDur) {
			StopBoost();
		}

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Death")) {
			rb.velocity = rb.velocity * 0.0f;
			GameObject temp = (GameObject)Instantiate(bigBoom, this.transform.position, Quaternion.identity); 
			source.PlayOneShot(death, 0.5f);
			lives--;
			if(lives > 0){
				rb.position = new Vector3(0.0f, 5f, 0.0f);
			} else {
				if(this.gameObject)
				{
					//Destroy(this.gameObject);
					this.gameObject.SetActive (false);
				}
				//this.gameObject.SetActive (false);
			}
		}
	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.CompareTag ("Player")) {
			GameObject temp = (GameObject)Instantiate(boostCloud, other.rigidbody.position, Quaternion.identity); 
			source.PlayOneShot(bump, 0.1f);
		}
	}

	void StartBoost (ControllerMessage msg) {
		if(msg.ControllerSource != cid){
			return;
		}
		inactiveTime = 0;
		rb.AddExplosionForce (boostSpeed, (rb.position - rb.velocity), 0.0f, 0.0f, ForceMode.Impulse);
		GameObject temp = (GameObject)Instantiate (boostCloud, rb.position, Quaternion.identity);
		//this.transform.localScale = 2f * this.transform.localScale;
		source.PlayOneShot (boostSound, 0.5f);
		boosting = true;
		boostCounter = 0;
	}

	void StopBoost () {
		rb.velocity = rb.velocity * 0.5f;
		//this.transform.localScale = this.transform.localScale / 2f;
		boosting = false;
	}

	void MovePlayer(ControllerMessage msg){
		if(msg.ControllerSource != cid){
			return;
		}
		inactiveTime = 0;
		float x = 0;
		float z = 0;
		if (msg.Payload.HasField ("beta")) {
			x = System.Convert.ToSingle(msg.Payload.GetField("beta").ToString());
		} else {
			print ("Vertical tilt not detected");
		}
		if (msg.Payload.HasField ("gamma")) {
			z = System.Convert.ToSingle(msg.Payload.GetField("gamma").ToString());
		} else {
			print ("Horizontal tilt not detected");
		}
		Vector3 move = new Vector3 (x, 0.0f, z);
		rb.AddForce (move * speed);
	}
}
