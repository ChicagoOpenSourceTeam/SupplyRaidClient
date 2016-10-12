using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	public const int SCENE_GAME_BOARD = 5;

	public GameObject errorDialog;
	private DisplayErrorDialog displayErrorDialog;
	public Button buttonStartGame;

	public void onClick() {
		buttonStartGame.enabled = false;
		StartCoroutine(makePostToStartGame());
		buttonStartGame.enabled = true;
		return;
	}

	IEnumerator makePostToStartGame ()
	{

		RESTClient<Object> client = new RESTClient<Object>();

		yield return client
			.SetEndpoint ("/game/start")
			.SetMethods (UnityWebRequest.kHttpVerbPOST)
			.sendRequest();

		client.handleResponse();

		if (client.responseCode == 200) {
			SceneManager.LoadScene (SCENE_GAME_BOARD);
		}
		else if (client.responseCode == 409) {
			displayErrorDialog.displayErrorMessage ("Too few players to start.");
		} else {
			displayErrorDialog.displayErrorMessage ("Unknown error. Try again later.");
		}
	}



	void Start () {
		displayErrorDialog = errorDialog.GetComponent<DisplayErrorDialog> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
