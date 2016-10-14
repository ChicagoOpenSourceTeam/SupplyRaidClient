using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Text;

public class GetBoard : MonoBehaviour {

	public BoardResponse boardResponse;
	public Canvas canvas;


	// Use this for initialization
	void Start () {
		StartCoroutine(download ());
	}
	
	// Update is called once per frame
	void Update () {
		if (boardResponse != null) {
			canvas.gameObject.SetActive (false);
//			canvas.GetComponent<Canvas> ().enabled = false;
		} 
//			else {
//			canvas.gameObject.SetActive (true);
//		}
	}


	private IEnumerator download()
	{
		RESTClient<object> client = new RESTClient<object>();

		yield return client.SetEndpoint ("/board").SetMethods (UnityWebRequest.kHttpVerbGET).sendRequest();

		client.handleResponse();


		if (client.responseCode == 200) {
			boardResponse = JsonUtility.FromJson<BoardResponse> (client.response);
		}

		StartCoroutine (waitAndTryAgain ());
	}	

	private IEnumerator waitAndTryAgain() {
		yield return new WaitForSeconds (2);
		StartCoroutine (download ());
	}



	[Serializable]
	public class BoardResponse {
		public int playerNumber;
		public TerritoryResponse[] territories;
		public PlayerResponse[] players;
	}

	[Serializable]
	public class TerritoryResponse {
		public String name;
		public int territoryId;
		public bool supplyDepot;
		public bool supplied;
		public int troops;
		public int playerNumber;
		public Link[] links;
	}

	[Serializable]
	public class PlayerResponse {
		public String name;
		public int playerNumber;
		public int troops;
		public int territories;
		public int supplyDepots;
		public Link[] links;
	}

	[Serializable]
	public class Link {
		public string href;
		public string rel;
	}

	
}
