using UnityEngine;
using System.Collections;

public class KeyboardController : MonoBehaviour
{
	public AudioClip attackSound;
	public AudioClip jumpSound;

	private AudioSource source;
	public float speed=2;
	public float jumpForce= 5;
	public bool lookRight = true;

	private Animator animator;
	private Rigidbody2D rb2D;
	public GameObject weapon;

	private bool grounded = true;
	private GameController gameController;
	//public Transform groundCheck;

	void Start ()
	{
		animator = GetComponent<Animator>();
		rb2D = GetComponent<Rigidbody2D> ();
		rb2D.freezeRotation = true;

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
		source = GetComponent<AudioSource>();
	}


	void Update () {

		weapon.GetComponent<BoxCollider2D>().enabled = false;
		Debug.DrawLine (transform.position, transform.position + Vector3.down*0.3f, Color.red);
		grounded = Physics2D.Linecast(transform.position, transform.position + Vector3.down*0.3f, 1 << LayerMask.NameToLayer("Ground"));

		if(Input.GetKeyDown("space"))
		{	
			weapon.GetComponent<BoxCollider2D>().enabled = true;

			if(Random.Range(0f, 1.0f) > 0.5f)
				animator.SetTrigger("attack");
			else
				animator.SetTrigger("special");
		}

		if (Input.GetKey ("right")) {
			if (!lookRight) {
				Flip ();
			}
			animator.SetFloat ("speed", speed);
			transform.Translate (Vector3.right * speed * Time.deltaTime);
			if (Input.GetKeyDown ("up") && grounded) {
				source.PlayOneShot(jumpSound,1.0f);
				rb2D.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
			}
		} else if (Input.GetKey ("left")) {
			if (lookRight) {
				Flip ();
			}
			animator.SetFloat ("speed", speed);
			transform.Translate (Vector3.left * speed * Time.deltaTime);
			if (Input.GetKeyDown ("up") && grounded) {
				source.PlayOneShot(jumpSound,1.0f);
				rb2D.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
			}
		}  
		else {
			animator.SetFloat ("speed", 0);
			if (Input.GetKeyDown ("up") && grounded) {
				source.PlayOneShot(jumpSound,1.0f);
				rb2D.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Impulse);
			}
		}
	}

	public void AttackSound(){
		source.PlayOneShot(attackSound,1.0f);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag.Contains("ground")) 
		{
			grounded = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Contains ("bullet")) {
			gameController.GameOver ();
			Destroy (gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag.Contains("Finish") && Input.GetKeyDown ("up")) {
			gameController.gameWin = true;
			gameController.GameOver ();
			Destroy(gameObject);
		}
	}

	void OnBecameInvisible() {
		gameController.GameOver ();
		Destroy(gameObject);
	}

	public void Flip()
	{
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		lookRight = !lookRight;
	}
}