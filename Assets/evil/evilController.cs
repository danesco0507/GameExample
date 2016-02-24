using UnityEngine;
using System.Collections;

public class evilController : MonoBehaviour {

	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");

		if (vertical > 0)
		{
			animator.SetInteger("Estado", 2);
		}
		else if (vertical < 0)
		{
			animator.SetInteger("Estado", 3);
		}
		else if (horizontal > 0)
		{
			animator.SetInteger("Estado", 0);
		}
		else if (horizontal < 0)
		{
			animator.SetInteger("Estado", 1);

		}
	}
}
