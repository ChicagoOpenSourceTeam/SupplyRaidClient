using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

public class RootMain : ContextView {

	// Use this for initialization
	void Start ()
    {
        //Instantiate the context, passing it this instance.
        context = new RootContext(this);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
