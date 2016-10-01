using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Text;
using Assets.Code;


public class GetPlayersFromServer : MonoBehaviour {

    string baseUrl;
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

    // Update is called once per frame
    void Update () {
	
	}


}
public class Player
{
    public int playerNumber;
    public String name;
    public String gameName;
    public int playerId;

}
