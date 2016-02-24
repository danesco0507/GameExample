using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	//Object to look at and follow.
	public Transform target;

	//Stops camera from moving past the border to the left.
	public float minimum = 4.8F;

	//Stops camera from moving past the border to the right.
	public float maximum = 9.7F;

	float x;

	private Vector2 velocity;

	public bool useSmoothing = false;

	void Start()
	{
	}

	void Update()
	{
		if (target != null) {	
			if (target.position.x < minimum)
				x = minimum;
			else if (target.position.x > maximum)
				x = maximum;
			else
				x = target.position.x;
					

			transform.position = new Vector3 (x, transform.position.y, transform.position.z);
		}
	}

	public void LateUpdate()
	{
		//Stop camera from moving in the y axis when mario jumps.
		Camera.main.transform.position = new Vector3(x, 0, transform.position.z);
	} 

}