using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour {
    public static int SCENE_CREATE_GAME = 0;

    // Use this for initialization
    public void onClick(){
        SceneManager.LoadScene(SCENE_CREATE_GAME);
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
