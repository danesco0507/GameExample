using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class MobileGameController : MonoBehaviour
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
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
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
		restartText.text = "Presiona la pantalla para reiniciar";
		restart = true;
	}

}


