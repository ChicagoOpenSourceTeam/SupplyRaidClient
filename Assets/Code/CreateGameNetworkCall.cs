using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CreateGameNetworkCall : MonoBehaviour {

	public const int SCENE_HOST_USER_NAME = 2;
    public static string GAME_NAME_KEY = "gameName";

    public GameObject errorDialog;
	private DisplayErrorDialog displayErrorDialog;
    public Button button;
	public InputField field;


	// Use this for initialization
	void Start () {
		displayErrorDialog = errorDialog.GetComponent<DisplayErrorDialog> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public IEnumerator upload() {
		CreateGameRequest createGameRequest = new CreateGameRequest ();
		createGameRequest.gameName = field.text;

		RESTClient<CreateGameRequest> client = new RESTClient<CreateGameRequest> ();

		yield return client
			.SetEndpoint ("/game")
			.SetMethods (UnityWebRequest.kHttpVerbPOST)
			.SetUploadData (createGameRequest)
			.sendRequest();

		client.handleResponse();

		if (client.responseCode == 200) {
            PlayerPrefs.SetString(GAME_NAME_KEY, createGameRequest.gameName);
			SceneManager.LoadScene (SCENE_HOST_USER_NAME);
        }
        else if (client.responseCode == 409) {
			displayErrorDialog.displayErrorMessage ("Game name already taken.");
		} else {
			displayErrorDialog.displayErrorMessage ("Unknown error. Try again later.");
		}

	}

	public void onClick() {
        button.enabled = false;

		StartCoroutine(upload ());
        button.enabled = true;
		return;
	}

	public class CreateGameRequest {
		public string gameName;
	}
}




