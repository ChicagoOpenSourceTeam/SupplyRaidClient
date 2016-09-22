using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LaunchJoinGameScreen : MonoBehaviour {

    public static int SCENE_JOIN_GAME = 4;


    public void onClick()
    {
        SceneManager.LoadScene(SCENE_JOIN_GAME);

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
