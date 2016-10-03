using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class RESTClient<RequestType> {

	private string baseUrl;
	private string method;
	private string endpoint;
	private RequestType uploadData;
	public long responseCode { set; get; }
	public UnityWebRequest webRequest { set; get; }
	public string response { get; set; }

	public RESTClient() {
		#if UNITY_EDITOR
		baseUrl = "http://localhost:8080";
		#elif UNITY_WEBGL
		baseUrl = "http://supply-attack-server.herokuapp.com";
		#endif

	}

	public RESTClient<RequestType> SetMethods(string method) {
		this.method = method;
		return this;
	}

	public RESTClient<RequestType> SetEndpoint(string endpoint) {
		this.endpoint = endpoint;
		return this;
	}

	public RESTClient<RequestType> SetUploadData(RequestType uploadData) {
		this.uploadData = uploadData;
		return this;
	}

	public void handleResponse() {
		this.responseCode = webRequest.responseCode;

		if (webRequest.GetResponseHeader ("X-Auth-Token") != null) {
			SessionHolder.sessionCookie = webRequest.GetResponseHeader("X-Auth-Token");
		}

		response = (Encoding.UTF8.GetString(webRequest.downloadHandler.data));
	}

	public AsyncOperation sendRequest() {
		webRequest = new UnityWebRequest (baseUrl + endpoint, method);

		if (uploadData != null) {
			string json = JsonUtility.ToJson (uploadData);
			UploadHandler uploadHandler = new UploadHandlerRaw (Encoding.UTF8.GetBytes (json));
			webRequest.uploadHandler = uploadHandler;
		}

		DownloadHandler downloadHandler = new DownloadHandlerBuffer ();
		webRequest.downloadHandler = downloadHandler;

		webRequest.SetRequestHeader ("Content-Type", "application/json");

		if (SessionHolder.sessionCookie != null && SessionHolder.sessionCookie != "") {
			webRequest.SetRequestHeader ("X-Auth-Token", SessionHolder.sessionCookie);
		}

		return webRequest.Send ();
	}
}