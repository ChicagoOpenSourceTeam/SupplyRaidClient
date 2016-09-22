using UnityEngine;using System.Collections;using System.Text;using UnityEngine.UI;using UnityEngine.Networking;using UnityEngine.SceneManagement;public class PostPlayerAndEnterWaitingRoomOnSuccess : MonoBehaviour {    public const int SCENE_PLAYER_WAITING_ROOM = 3;    string baseUrl;    public GameObject errorDialog;    private DisplayErrorDialog displayErrorDialog;    public Button button;    public InputField field;    // Update is called once per frame    void Update () {		}    public IEnumerator upload()    {        UnityWebRequest webRequest = new UnityWebRequest(baseUrl + "/players", UnityWebRequest.kHttpVerbPOST);        CreatePlayerRequest createPlayerRequest = new CreatePlayerRequest();        createPlayerRequest.playerName = field.text;        createPlayerRequest.gameName = PlayerPrefs.GetString(CreateGameNetworkCall.GAME_NAME_KEY);        string json = JsonUtility.ToJson(createPlayerRequest);        UploadHandler uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));        DownloadHandler downloadHandler = new DownloadHandlerBuffer();        webRequest.uploadHandler = uploadHandler;        webRequest.downloadHandler = downloadHandler;        webRequest.SetRequestHeader("Content-Type", "application/json");        yield return webRequest.Send();

        displayErrorDialog = errorDialog.GetComponent<DisplayErrorDialog>();        if (webRequest.responseCode == 200)        {            PlayerPrefs.SetString("hostPlayerName", createPlayerRequest.playerName);            SceneManager.LoadScene(SCENE_PLAYER_WAITING_ROOM);        }        else if (webRequest.responseCode == 409)        {            displayErrorDialog.displayErrorMessage("Player name already taken.");        }        else        {            displayErrorDialog.displayErrorMessage("Unknown error. Try again later.");        }    }    public void onClick()    {        button.enabled = false;        StartCoroutine(upload());        button.enabled = true;        return;    }


    void Start()
    {
        #if UNITY_EDITOR
                baseUrl = "http://localhost:8080";
        #elif UNITY_WEBGL
		        baseUrl = "http://supply-attack-server.herokuapp.com";
        #endif
    }    public class CreatePlayerRequest    {        public string playerName;        public string gameName;    }}