using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

public class GetGameFromServer : MonoBehaviour {

	public static int GAME_BOARD_SCENE = 5;
    string baseUrl;

    // Use this for initialization
    void Start () {
        StartCoroutine(download());
	}

	private IEnumerator download()
    {
		RESTClient<object> client = new RESTClient<object>();

		yield return client.SetEndpoint ("/game").SetMethods (UnityWebRequest.kHttpVerbGET).sendRequest();

		client.handleResponse ();

		if (client.responseCode == 200) {
			GameResponse gameResponse = JsonUtility.FromJson<GameResponse>(client.response);

			if (gameResponse.gameStarted) {
				SceneManager.LoadScene (GAME_BOARD_SCENE);
			}
		}

		StartCoroutine (waitAndTryAgain ());
    }	

	private IEnumerator waitAndTryAgain() {
		yield return new WaitForSeconds (2);
		StartCoroutine (download ());
	}

    // Update is called once per frame
    void Update () {
	
	}

	[Serializable]
	public class GameResponse {
		public bool gameStarted;
	}
}
