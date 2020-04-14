using System;
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
    }

    public void OnAcceptButtonPushed()
    {
        //Destroy(this.gameObject);

        //this.gameObject.AddComponent<SimpleAd>();
        //DialogSpawner.dialogSpawner.SpawnConfirmationDialog("Spawned another confirmation dialog!");
        if(!AudioManager.audioManager.mute) { this.GetComponentInParent<AudioSource>().Play(); }
        if(acceptAction != null) { acceptAction.Invoke(); }
        Destroy(this.gameObject);
    }

    public void OnDeclineButtonPushed()
    {
        //DialogSpawner.dialogSpawner.SpawnErrorDialog("Spawned an error dialog!");
        if (!AudioManager.audioManager.mute) { this.GetComponentInParent<AudioSource>().Play(); }
        if (declineAction != null) { declineAction.Invoke(); }
        Destroy(this.gameObject);
    }
}
