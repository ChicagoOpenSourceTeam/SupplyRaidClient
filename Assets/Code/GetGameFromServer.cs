using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System;

public class GetGameFromServer : MonoBehaviour {

    string baseUrl;

    // Use this for initialization
    void Start () {
        StartCoroutine(download());
	}

	private IEnumerator download()
    {
		RESTClient<object> client = new RESTClient<object>();

		yield return client.SetEndpoint ("/game").SetMethods (UnityWebRequest.kHttpVerbGET).sendRequest();

		StartCoroutine (waitAndTryAgain ());
    }	

	private IEnumerator waitAndTryAgain() {
		yield return new WaitForSeconds (2);
		StartCoroutine (download ());
	}

    // Update is called once per frame
    void Update () {
	
	}
}
