﻿using UnityEngine;

        displayErrorDialog = errorDialog.GetComponent<DisplayErrorDialog>();


    void Start()
    {
        #if UNITY_EDITOR
                baseUrl = "http://localhost:8080";
        #elif UNITY_WEBGL
		        baseUrl = "http://supply-attack-server.herokuapp.com";
        #endif
    }