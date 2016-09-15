using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CreateGameNetworkCall : MonoBehaviour {

	public InputField field;
	string baseUrl;

	// Use this for initialization
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


	public IEnumerator upload() {
		UnityWebRequest webRequest = new UnityWebRequest (baseUrl + "/game", UnityWebRequest.kHttpVerbPOST);
		UploadHandler uploadHandler = new UploadHandlerRaw (Encoding.UTF8.GetBytes("{\"gameName\":\""+field.text+"\"}"));
		DownloadHandler downloadHandler = new DownloadHandlerBuffer ();
		webRequest.uploadHandler = uploadHandler;
		webRequest.downloadHandler = downloadHandler;
		webRequest.SetRequestHeader ("Content-Type", "application/json");

		yield return webRequest.Send ();
	}

	public void onClick() {
		StartCoroutine(upload ());
		return;
	}
}
