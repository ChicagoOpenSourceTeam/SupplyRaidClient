using UnityEngine;
using System.Collections;

public class GenericRESTHandler : MonoBehaviour
{
    private string results;

    // Use this for initialization
    void Start()
    {
        results = "No results gathered so far.";
    }

    // Update is called once per frame
    void Update() { }

    public string Results
    {
        get
        {
            return results;
        }
    }

    public WWW GET(string url, System.Action onComplete)
    {

        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, onComplete));
        return www;
    }

    public WWW POST(string url, string fieldName, string value, System.Action onComplete)
    {
        WWWForm form = new WWWForm();
        form.AddField(fieldName, value);

        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www, onComplete));
        return www;
    }

    private IEnumerator WaitForRequest(WWW www, System.Action onComplete)
    {
        yield return www;

        if (www.error == null)
        {
            results = www.text;
            onComplete();
        }
        else
        {
            Debug.Log(www.error);
        }
    }
}
