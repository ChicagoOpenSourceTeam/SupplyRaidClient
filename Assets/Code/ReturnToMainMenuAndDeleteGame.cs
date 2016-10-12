using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ReturnToMainMenuAndDeleteGame : MonoBehaviour {
    public static int SCENE_MAIN_MENU = 0;


    // Use this for initialization
    public void onClick() {
        StartCoroutine(delete());
    }


    private IEnumerator delete() {
        string gameName = PlayerPrefs.GetString(CreateGameNetworkCall.GAME_NAME_KEY);

		RESTClient<object> client = new RESTClient<object>();

		yield return client
			.SetEndpoint ("/game/"+gameName)
			.SetMethods (UnityWebRequest.kHttpVerbDELETE)
			.sendRequest();

		client.handleResponse();

        if (client.responseCode == 200 || client.responseCode == 404)
        {
            SceneManager.LoadScene(SCENE_MAIN_MENU);
        }

    }

    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
