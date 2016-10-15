using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpposingPlayerDisplay : MonoBehaviour {

	public GameObject boardGetter;

	public Sprite player1Emblem;
	public Sprite player2Emblem;
	public Sprite player3Emblem;
	public Sprite player4Emblem;

	public int boardNumber;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetBoard board = boardGetter.GetComponent<GetBoard> ();

		int playerNumber = board.boardResponse.playerNumber;
		int numPlayers = board.boardResponse.players.Length;

		if (numPlayers == 2 && boardNumber == 1) { gameObject.SetActive (false); }
		if (numPlayers == 2 && boardNumber == 2) { gameObject.SetActive (true); }
		if (numPlayers == 2 && boardNumber == 3) { gameObject.SetActive (false); }
		if (numPlayers == 3 && boardNumber == 1) { gameObject.SetActive (true); }
		if (numPlayers == 3 && boardNumber == 2) { gameObject.SetActive (false); }
		if (numPlayers == 3 && boardNumber == 3) { gameObject.SetActive (true); }
		if (numPlayers == 4 && boardNumber == 1) { gameObject.SetActive (true); }
		if (numPlayers == 4 && boardNumber == 2) { gameObject.SetActive (true); }
		if (numPlayers == 4 && boardNumber == 3) { gameObject.SetActive (true); }

		int opposingPlayerNumber = 0;
		foreach (GetBoard.PlayerResponse player in board.boardResponse.players) {
			if (player.playerNumber == playerNumber) { continue; }

			opposingPlayerNumber++;

			if ((boardNumber == 1 && numPlayers == 3 && opposingPlayerNumber == 1) ||
			    (boardNumber == 1 && numPlayers == 4 && opposingPlayerNumber == 1) ||
			    (boardNumber == 2 && numPlayers == 2 && opposingPlayerNumber == 1) ||
			    (boardNumber == 2 && numPlayers == 4 && opposingPlayerNumber == 2) ||
			    (boardNumber == 3 && numPlayers == 3 && opposingPlayerNumber == 2) ||
			    (boardNumber == 3 && numPlayers == 4 && opposingPlayerNumber == 3)) {

				if (player.playerNumber == 1) {
					transform.FindChild ("PlayerIcon").GetComponent<SpriteRenderer> ().sprite = player1Emblem;
				} else if (player.playerNumber == 2) {
					transform.FindChild ("PlayerIcon").GetComponent<SpriteRenderer> ().sprite = player2Emblem;
				} else if (player.playerNumber == 3) {
					transform.FindChild ("PlayerIcon").GetComponent<SpriteRenderer> ().sprite = player3Emblem;
				} else if (player.playerNumber == 4) {
					transform.FindChild ("PlayerIcon").GetComponent<SpriteRenderer> ().sprite = player4Emblem;
				}

				transform.FindChild ("TerritoriesNumber").GetComponent<Text> ().text = player.territories.ToString ();
				transform.FindChild ("TroopsNumber").GetComponent<Text> ().text = player.troops.ToString ();
				transform.FindChild ("SupplyDepotsNumber").GetComponent<Text> ().text = player.supplyDepots.ToString ();
			}
		}
	}
}
