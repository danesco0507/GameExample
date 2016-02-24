using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	public AudioClip shootSound;
	public bool facingRight = true;
	public float moveSpeed = 1f;
	private Animator animator;
	private Rigidbody2D rb2D;
	private Shot state;
	private Die deadState;
	public bool alive = true;

	public GameObject bullet;

	private float alertDistance = 8f;
	private float shotDistance = 5f;
	private AudioSource source;

	GameObject player;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		state = animator.GetBehaviour<Shot> ();
		deadState = animator.GetBehaviour<Die> ();
		deadState.character = this.gameObject;
		rb2D = GetComponent<Rigidbody2D> ();
		rb2D.freezeRotation = true;
		player = GameObject.Find("Player");
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3(moveSpeed,0,0)*Time.deltaTime) ;
		if (player != null) {
			var enemy2Player = player.transform.position - transform.position;

			if (state.moving)
				ResumeMoving ();
			else
				StopMoving ();

			if (!alive) {
				deadState.dead = !alive;
				state.moving = alive;
				animator.SetInteger ("Estado", 3);
			} else if ((facingRight && enemy2Player.x >= 0f && Mathf.Abs (enemy2Player.x) <= shotDistance && Mathf.Abs (enemy2Player.y) < 1) || (!facingRight && enemy2Player.x <= 0f && Mathf.Abs (enemy2Player.x) <= shotDistance && Mathf.Abs (enemy2Player.y) < 1)) {
				animator.SetInteger ("Estado", 2);
			} else if (enemy2Player.sqrMagnitude > alertDistance * alertDistance) {
				animator.SetInteger ("Estado", 0);
			} else if (enemy2Player.sqrMagnitude <= alertDistance * alertDistance) {
				animator.SetInteger ("Estado", 1);
			}
		} else {
			Destroy (gameObject);
		}
	}

	void shot(){
		GameObject obj;
		//yield return new WaitForSeconds (0.3f);
		Vector3 correction;
		if (facingRight) {
			correction = new Vector3(1f,0.28f,0);
			obj = (GameObject)Instantiate (bullet, transform.position + correction, Quaternion.Euler (0, 0, 90));
		}
		else {
			correction = new Vector3(-1f,0.28f,0);
			obj = (GameObject)Instantiate (bullet, transform.position + correction, Quaternion.Euler (0, 0, 90));
			obj.GetComponent<BulletScript> ().speed *= -1;
		}
		source.PlayOneShot(shootSound,1.0f);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag.Contains ("edge")) {
			Flip ();
			moveSpeed *= -1;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "orc_weapon") {
			alive = false;
		}
	}

	public void ResumeMoving(){
		if (facingRight)
			moveSpeed = 1f;
		else
			moveSpeed = -1f;
	}

	public void StopMoving(){
		moveSpeed = 0f;
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
