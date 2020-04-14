﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class ConfirmationDialog : MonoBehaviour
{
    public string confirmationMessage;
    public Action acceptAction = null;
    public Action declineAction = null;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Text>().text = confirmationMessage;

        if (GameObject.FindGameObjectsWithTag("GreyBackground").Length > 1)
        {
            GameObject greyBackground = GameObject.FindGameObjectsWithTag("GreyBackground")[1];
            Destroy(greyBackground);
        }
    }

    public void OnAcceptButtonPushed()
    {
        if(!AudioManager.audioManager.mute) { this.GetComponentInParent<AudioSource>().Play(); }
        if(acceptAction != null) { acceptAction.Invoke(); }
        Destroy(this.gameObject);
    }

    public void OnDeclineButtonPushed()
    {
        if (!AudioManager.audioManager.mute) { this.GetComponentInParent<AudioSource>().Play(); }
        if (declineAction != null) { declineAction.Invoke(); }
        Destroy(this.gameObject);
    }
}
