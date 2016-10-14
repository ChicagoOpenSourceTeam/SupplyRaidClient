using UnityEngine;
using System.Collections;

public class DisplayTerritory : MonoBehaviour {

	public Sprite unoccupied;
	public Sprite unsupplied;
	public Sprite supplied;

	public GameObject boardGetter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetBoard board = boardGetter.GetComponent<GetBoard> ();
		foreach (GetBoard.TerritoryResponse territory in board.boardResponse.territories) {
			if (territory.name.Equals (name)) {
				if (territory.playerNumber == 0) {
					GetComponent<SpriteRenderer> ().sprite = unoccupied;
				} else if (territory.supplied == false) {
					GetComponent<SpriteRenderer> ().sprite = unsupplied;
				} else {
					GetComponent<SpriteRenderer> ().sprite = supplied;
				}
				return;
			}
		}
	}
}
