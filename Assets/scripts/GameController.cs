using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameController : MonoBehaviour
{
	public Text restartText;
	public Text gameOverText;

	private AudioSource source;
	public AudioClip finishSound;
	public AudioClip lostSound;

	public bool gameWin;
	public bool restart;

	void Start(){
		source = GetComponent<AudioSource>();
		gameWin = false;
		restart = false;

		restartText.text = "";
		gameOverText.text = "";
	}

	void Update(){
		if (restart)
		{
			#if UNITY_EDITOR
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}

			#elif UNITY_IPHONE || UNITY_ANDROID

			if (Input.touchCount == 0){
				Application.LoadLevel (Application.loadedLevel);
			}

			#endif
		}
	}

	public void GameOver(){
		source.Stop();
		if (!gameWin) {
			source.PlayOneShot(lostSound,1.0f);
			gameOverText.text = "¡Perdiste!";
		} else{
			source.PlayOneShot(finishSound,1.0f);
			gameOverText.text = "¡Ganaste!";
		}

		#if UNITY_EDITOR
			restartText.text = "Presione 'R' para reiniciar";
		#elif UNITY_IPHONE || UNITY_ANDROID
			restartText.text = "Presione la pantalla para reiniciar";
		#endif
		restart = true;
	}
		
}


