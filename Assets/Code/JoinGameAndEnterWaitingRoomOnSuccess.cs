using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class JoinGameAndEnterWaitingRoomOnSuccess : MonoBehaviour {

    public const int SCENE_PLAYER_WAITING_ROOM = 3;

    string baseUrl;
    public GameObject errorDialog;
    private DisplayErrorDialog displayErrorDialog;
    public Button button;
    public InputField gameField;
    public InputField playerField;

    // Use this for initialization
    public IEnumerator upload()
    {
        UnityWebRequest webRequest = new UnityWebRequest(baseUrl + "/players", UnityWebRequest.kHttpVerbPOST);
        CreatePlayerRequest createPlayerRequest = new CreatePlayerRequest();
        createPlayerRequest.playerName = playerField.text;
        createPlayerRequest.gameName = gameField.text;
        string json = JsonUtility.ToJson(createPlayerRequest);

        UploadHandler uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
        DownloadHandler downloadHandler = new DownloadHandlerBuffer();
        webRequest.uploadHandler = uploadHandler;
        webRequest.downloadHandler = downloadHandler;
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.Send();

        displayErrorDialog = errorDialog.GetComponent<DisplayErrorDialog>();

        if (webRequest.responseCode == 200)
        {
            SceneManager.LoadScene(SCENE_PLAYER_WAITING_ROOM);
        }
        else if (webRequest.responseCode == 409)
        {
            displayErrorDialog.displayErrorMessage(Encoding.UTF8.GetString(webRequest.downloadHandler.data));
        }

        else if (webRequest.responseCode == 404)
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
        #if UNITY_EDITOR
                baseUrl = "http://localhost:8080";
        #elif UNITY_WEBGL
		        baseUrl = "http://supply-attack-server.herokuapp.com";
        #endif

    }

    public class CreatePlayerRequest
    {
        public string playerName;
        public string gameName;
    }
}
