using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayErrorDialog : MonoBehaviour {

	public Text message;
	public CanvasGroup canvasGroup;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void displayErrorMessage(string message) {
		this.message.text = message;
		StartCoroutine (FadeInAndOut());
	}
	
	IEnumerator FadeInAndOut()
	{
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 0.5f)
		{
			yield return new WaitForSeconds (.01f);
			canvasGroup.alpha = t;
		}
		for (float t = 0; t < 1.0f; t += Time.deltaTime / 2.0f) 
		{
			yield return new WaitForSeconds (.01f);
		}
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 0.5f)
		{
			yield return new WaitForSeconds (.01f);
			canvasGroup.alpha = 1.0f - t;
		}
		yield return null;
	}
}
