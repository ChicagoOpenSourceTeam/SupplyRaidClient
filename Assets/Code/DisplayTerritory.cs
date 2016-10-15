using UnityEngine;
using System.Collections;

public class DisplayTerritory : MonoBehaviour {

	public Sprite unoccupied;
	public Sprite unsupplied;
	public Sprite supplied;
	public Sprite player1Emblem;
	public Sprite player2Emblem;
	public Sprite player3Emblem;
	public Sprite player4Emblem;

	public GameObject boardGetter;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetBoard board = boardGetter.GetComponent<GetBoard> ();
//		if (board.boardResponse.territories.Length == 0) {
//			GetComponent<SpriteRenderer>().enabled = false;
//			transform.FindChild ("Emblem").GetComponent<SpriteRenderer>().enabled = false;
//		} else {
//			GetComponent<SpriteRenderer>().enabled = true;
//			transform.FindChild ("Emblem").GetComponent<SpriteRenderer>().enabled = true;
//		}
		foreach (GetBoard.TerritoryResponse territory in board.boardResponse.territories) {
			if (territory.name.Equals (name)) {
				if (territory.playerNumber == 0) {
					GetComponent<SpriteRenderer> ().sprite = unoccupied;
				} else if (territory.supplied == false) {
					GetComponent<SpriteRenderer> ().sprite = unsupplied;
				} else {
					GetComponent<SpriteRenderer> ().sprite = supplied;
				}
				if (territory.supplyDepot == true) {
					transform.FindChild ("SupplyIcon").gameObject.SetActive (true);
				} else {
					transform.FindChild ("SupplyIcon").gameObject.SetActive (false);
				}

				if (territory.playerNumber == 1) {
					transform.FindChild ("Emblem").gameObject.SetActive (true);
					transform.FindChild ("Emblem").GetComponent<SpriteRenderer> ().sprite = player1Emblem;
				} else if (territory.playerNumber == 2) {
					transform.FindChild ("Emblem").gameObject.SetActive (true);
					transform.FindChild ("Emblem").GetComponent<SpriteRenderer> ().sprite = player2Emblem;
				} else if (territory.playerNumber == 3) {
					transform.FindChild ("Emblem").gameObject.SetActive (true);
					transform.FindChild ("Emblem").GetComponent<SpriteRenderer> ().sprite = player3Emblem;
				} else if (territory.playerNumber == 4) {
					transform.FindChild ("Emblem").gameObject.SetActive (true);
					transform.FindChild ("Emblem").GetComponent<SpriteRenderer> ().sprite = player4Emblem;
				} else {
					transform.FindChild ("Emblem").gameObject.SetActive (false);
				}

				return;
			}
		}
	}
}
