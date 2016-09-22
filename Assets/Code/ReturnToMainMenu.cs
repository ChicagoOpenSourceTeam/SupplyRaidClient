using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ReturnToMainMenu : MonoBehaviour {
    public static int SCENE_MAIN_MENU = 0;

    // Use this for initialization
    void Start () {
	
	}

    public void onClick()
    {
        SceneManager.LoadScene(SCENE_MAIN_MENU);

    }

    // Update is called once per frame
    void Update () {
	
	}
}
