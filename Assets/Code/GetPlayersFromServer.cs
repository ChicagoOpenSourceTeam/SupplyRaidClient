using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

public class GetPlayersFromServer : MonoBehaviour {

    string baseUrl;
    public GameObject errorDialog;
    private DisplayErrorDialog displayErrorDialog;

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
        Debug.Log("Inside of Coroutine");
        while (true)
        {
            Debug.Log("Inside of while loop.");
            download();
            yield return new WaitForSeconds(2);
        }
    }



    private void download()
    {
        Debug.Log("Inside download");
        UnityWebRequest webRequest = new UnityWebRequest(baseUrl + "/players", UnityWebRequest.kHttpVerbGET);
        DownloadHandler downloadHandler = new DownloadHandlerBuffer();
        webRequest.downloadHandler = downloadHandler;
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.Send();

    }

    // Update is called once per frame
    void Update () {
	
	}


}
