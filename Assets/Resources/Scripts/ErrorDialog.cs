﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ErrorDialog : MonoBehaviour
{
    public string errorMessage;
    public Action closeAction = null;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Text>().text = errorMessage;

        if (GameObject.FindGameObjectsWithTag("GreyBackground").Length > 1)
        {
            GameObject greyBackground = GameObject.FindGameObjectsWithTag("GreyBackground")[1];
            Destroy(greyBackground);
        }
    }

    public void OnCloseButtonPushed()
    {
        if (!AudioManager.audioManager.mute) { this.GetComponentInParent<AudioSource>().Play(); }
        if (closeAction != null) { closeAction.Invoke(); }
        Destroy(this.gameObject);
    }
}
