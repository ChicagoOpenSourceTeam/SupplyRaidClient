using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Text;
using Assets.Code;


public class GetPlayersFromServer : MonoBehaviour {

    string baseUrl;
<<<<<<< HEAD
    public GameObject errorDialog;
    private DisplayErrorDialog displayErrorDialog;
    private Player[] players;

    // Use this for initialization
    void Start () {
#if UNITY_EDITOR
        baseUrl = "http://localhost:8080";
#elif UNITY_WEBGL
        baseUrl = "http://supply-attack-server.herokuapp.com";
#endif

        StartCoroutine(MakeGETCall());
	}

    

    private IEnumerator MakeGETCall()
    {
        while (true)
        {
            download();
            yield return new WaitForSeconds(2);
        }
    }
=======
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
>>>>>>> features/display-player-names-in-lobby

		yield return client.SetEndpoint ("/players").SetMethods (UnityWebRequest.kHttpVerbGET).sendRequest();

		client.handleResponse();

<<<<<<< HEAD
    private void download()
    {
        UnityWebRequest webRequest = new UnityWebRequest(baseUrl + "/players", UnityWebRequest.kHttpVerbGET);
        webRequest.
        DownloadHandler downloadHandler = new DownloadHandlerBuffer();
        webRequest.downloadHandler = downloadHandler;
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.Send();

        Debug.Log(webRequest.responseCode);
        Debug.Log((Encoding.UTF8.GetString(webRequest.downloadHandler.data)));
        players = JsonHelper.getJsonArray<Player>(Encoding.UTF8.GetString(webRequest.downloadHandler.data));

        printPlayer(players[0]);

    }

    public void printPlayer(Player playerInfo)
    {
        Debug.Log(playerInfo.name);
    }
=======
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
>>>>>>> features/display-player-names-in-lobby

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
public class Player
{
    public int playerNumber;
    public String name;
    public String gameName;
    public int playerId;

}
