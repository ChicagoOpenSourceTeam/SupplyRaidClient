using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PostPlayerAndEnterWaitingRoomOnSuccess : MonoBehaviour {

    public const int SCENE_PLAYER_WAITING_ROOM = 3;

	public GameObject errorDialog;
    private DisplayErrorDialog displayErrorDialog;
    public Button button;
    public InputField field;




    // Update is called once per frame
    void Update () {
	
	}



    public IEnumerator upload()
	{
        CreatePlayerRequest createPlayerRequest = new CreatePlayerRequest();
        createPlayerRequest.playerName = field.text;
        createPlayerRequest.gameName = PlayerPrefs.GetString(CreateGameNetworkCall.GAME_NAME_KEY);

		RESTClient<CreatePlayerRequest> client = new RESTClient<CreatePlayerRequest>();

		yield return client
			.SetEndpoint ("/players")
			.SetMethods (UnityWebRequest.kHttpVerbPOST)
			.SetUploadData (createPlayerRequest)
			.sendRequest();

		client.handleResponse();

        displayErrorDialog = errorDialog.GetComponent<DisplayErrorDialog>();

        if (client.responseCode == 200)
        {
            PlayerPrefs.SetString("isHost", "true");
            SceneManager.LoadScene(SCENE_PLAYER_WAITING_ROOM);
        }
        else if (client.responseCode == 409)
        {
            displayErrorDialog.displayErrorMessage("Player name already taken.");
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


    void Start()
    {
    }

    public class CreatePlayerRequest
    {
        public string playerName;
        public string gameName;
    }
}

