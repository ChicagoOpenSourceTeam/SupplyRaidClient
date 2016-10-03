using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System;

public class GetPlayersFromServer : MonoBehaviour {

    string baseUrl;
	public Text player1Text;
	public Text player2Text;
	public Text player3Text;
	public Text player4Text;

    // Use this for initialization
    void Start () {
        StartCoroutine(download());
	}

	private IEnumerator download()
    {
		RESTClient<object> client = new RESTClient<object>();

		yield return client.SetEndpoint ("/players").SetMethods (UnityWebRequest.kHttpVerbGET).sendRequest();

		client.handleResponse();

		if (client.responseCode == 200) {
			PlayerResponse[] playerResponses = JSONHelper.getJsonArray<PlayerResponse>(client.response);
			if (playerResponses.Length >= 1) {
				player1Text.text = "1. " + playerResponses [0].name;
			}
			if (playerResponses.Length >= 2) {
				player2Text.text = "2. " + playerResponses [1].name;
			}
			if (playerResponses.Length >= 3) {
				player3Text.text = "3. " + playerResponses [2].name;
			}
			if (playerResponses.Length >= 4) {
				player4Text.text = "4. " + playerResponses [3].name;
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
	public class PlayerResponse {
		public string name;
		public int playerNumber;
		public Link[] links;
	}

	[Serializable]
	public class Link {
		public string href;
		public string rel;
	}
}
