using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LaunchCreateGameScreen : MonoBehaviour {

	public static int SCENE_CREATE_GAME = 1;

	public void onClick() {
		SceneManager.LoadScene(SCENE_CREATE_GAME);
	}
}
