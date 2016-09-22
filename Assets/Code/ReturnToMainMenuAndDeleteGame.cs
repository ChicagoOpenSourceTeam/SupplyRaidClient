﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ReturnToMainMenuAndDeleteGame : MonoBehaviour {
    public static int SCENE_MAIN_MENU = 0;
    string baseUrl;


    // Use this for initialization
    public void onClick() {
        Debug.Log("onClick Called");
        StartCoroutine(delete());
    }


    private IEnumerator delete() {
        Debug.Log("Delete Called");
        string gameName = PlayerPrefs.GetString("gameName");

        UnityWebRequest webRequest = new UnityWebRequest(baseUrl + "/game/" + gameName, UnityWebRequest.kHttpVerbDELETE);
        
        DownloadHandler downloadHandler = new DownloadHandlerBuffer();
        webRequest.downloadHandler = downloadHandler;
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.Send();

        Debug.Log("Server responded");

        if (webRequest.responseCode == 200)
        {
            Debug.Log("Success!");
            SceneManager.LoadScene(SCENE_MAIN_MENU);
        }

        // || webRequest.responseCode == 404

    }

    void Start () {
		#if UNITY_EDITOR
			baseUrl = "http://localhost:8080";
		#elif UNITY_WEBGL
			baseUrl = "http://supply-attack-server.herokuapp.com";
		#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}