using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThisPlayerDisplay : MonoBehaviour {

	public GameObject boardGetter;

	public Sprite player1Icon;
	public Sprite player2Icon;
	public Sprite player3Icon;
	public Sprite player4Icon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetBoard board = boardGetter.GetComponent<GetBoard> ();

		int playerNumber = board.boardResponse.playerNumber;
		foreach (GetBoard.PlayerResponse player in board.boardResponse.players) {
			if (player.playerNumber == playerNumber) {
				transform.FindChild ("PlayerName").GetComponent<Text> ().text = player.name;
				if (playerNumber == 1) {
					transform.FindChild ("PlayerIcon").GetComponent<SpriteRenderer> ().sprite = player1Icon;
				} else if (playerNumber == 2) {
					transform.FindChild ("PlayerIcon").GetComponent<SpriteRenderer> ().sprite = player2Icon;
				} else if (playerNumber == 3) {
					transform.FindChild ("PlayerIcon").GetComponent<SpriteRenderer> ().sprite = player3Icon;
				} else {
					transform.FindChild ("PlayerIcon").GetComponent<SpriteRenderer> ().sprite = player4Icon;
				}
				transform.FindChild ("TroopsNumber").GetComponent<Text> ().text = player.troops.ToString();
				transform.FindChild ("TerritoriesNumber").GetComponent<Text> ().text = player.territories.ToString();
				transform.FindChild ("SupplyDepotsNumber").GetComponent<Text> ().text = player.supplyDepots.ToString();

			}
		}
	}
}
