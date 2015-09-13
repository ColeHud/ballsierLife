using UnityEngine;
using System.Collections;

public class DummyController : MonoBehaviour {
	public GameObject bigBoom;
	public AudioClip death;

	private Rigidbody rb;
	private AudioSource source;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		source = GetComponent<AudioSource> ();
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Death")){
			rb.velocity = rb.velocity * 0.0f;
			GameObject temp = (GameObject)Instantiate(bigBoom, this.transform.position, Quaternion.identity); 
			source.PlayOneShot(death, 1f);
			rb.position = new Vector3(0.0f, 5f, 0.0f);
		}
	}
}
