using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class JoinGameAndEnterWaitingRoomOnSuccess : MonoBehaviour {

    public const int SCENE_PLAYER_WAITING_ROOM = 3;

    public GameObject errorDialog;
    private DisplayErrorDialog displayErrorDialog;
    public Button button;
    public InputField gameField;
    public InputField playerField;

    // Use this for initialization
    public IEnumerator upload()
    {
        CreatePlayerRequest createPlayerRequest = new CreatePlayerRequest();
        createPlayerRequest.playerName = playerField.text;
        createPlayerRequest.gameName = gameField.text;

		RESTClient<CreatePlayerRequest> client = new RESTClient<CreatePlayerRequest> ();

		yield return client
			.SetEndpoint ("/players")
			.SetMethods (UnityWebRequest.kHttpVerbPOST)
			.SetUploadData (createPlayerRequest)
			.sendRequest();

		client.handleResponse();

        displayErrorDialog = errorDialog.GetComponent<DisplayErrorDialog>();

        if (client.responseCode == 200)
        {
			PlayerPrefs.SetString("isHost", "false");
            SceneManager.LoadScene(SCENE_PLAYER_WAITING_ROOM);
        }
		else if (client.responseCode == 409)
        {
			displayErrorDialog.displayErrorMessage(client.response);
        }

		else if (client.responseCode == 404)
        {
            displayErrorDialog.displayErrorMessage("Game Not Found. Check spelling.");
        }

        else
        {
            displayErrorDialog.displayErrorMessage("Unknown error. Try again later.");
        }

    }

    public void onClick()
    {
        button.enabled = false;

        StartCoroutine(upload());
        button.enabled = true;
        return;
    }

    // Update is called once per frame
    void Update () {
	
	}

    void Start()
    {

    }

    public class CreatePlayerRequest
    {
        public string playerName;
        public string gameName;
    }
}
