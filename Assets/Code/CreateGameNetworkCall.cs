using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CreateGameNetworkCall : MonoBehaviour {

	public const int SCENE_HOST_USER_NAME = 2;

    string baseUrl;
    public GameObject errorDialog;
	private DisplayErrorDialog displayErrorDialog;
    public Button button;
	public InputField field;


	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR
			baseUrl = "http://localhost:8080";
		#elif UNITY_WEBGL
			baseUrl = "http://supply-attack-server.herokuapp.com";
		#endif

		displayErrorDialog = errorDialog.GetComponent<DisplayErrorDialog> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public IEnumerator upload() {
		UnityWebRequest webRequest = new UnityWebRequest (baseUrl + "/game", UnityWebRequest.kHttpVerbPOST);
		CreateGameRequest createGameRequest = new CreateGameRequest ();
		createGameRequest.gameName = field.text;
		string json = JsonUtility.ToJson (createGameRequest);

		UploadHandler uploadHandler = new UploadHandlerRaw (Encoding.UTF8.GetBytes(json));
		DownloadHandler downloadHandler = new DownloadHandlerBuffer ();
		webRequest.uploadHandler = uploadHandler;
		webRequest.downloadHandler = downloadHandler;
		webRequest.SetRequestHeader ("Content-Type", "application/json");

		yield return webRequest.Send ();

		if (webRequest.responseCode == 200) {
            PlayerPrefs.SetString("gameName", createGameRequest.gameName);
			SceneManager.LoadScene (SCENE_HOST_USER_NAME);
        }
        else if (webRequest.responseCode == 409) {
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




