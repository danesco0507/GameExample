using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	Rigidbody2D rb2D;
	public float speed=4f;
	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
		rb2D.velocity = new Vector2(speed,0);
	}
	
	// Update is called once per frame
	void Update() {  

	}

	void OnBecameInvisible() {  
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player")
			Destroy(gameObject);
	}
}
