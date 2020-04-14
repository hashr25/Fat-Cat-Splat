using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogSpawner : MonoBehaviour
{
    public static DialogSpawner dialogSpawner;

    public GameObject errorDialogPrefab;
    public GameObject confirmationDialogPrefab;

    private void Start()
    {
        if (dialogSpawner == null)
        {
            DontDestroyOnLoad(gameObject);
            dialogSpawner = this;
        }
        else if (dialogSpawner != this)
        {
            Destroy(gameObject);
        }
    }

    public void SpawnErrorDialog(string errorMessage)
    {
        Canvas gui = FindGUICanvas();

        GameObject errorDialog = Instantiate(errorDialogPrefab);
        errorDialog.transform.SetParent(gui.transform, false);
        errorDialog.GetComponent<ErrorDialog>().errorMessage = errorMessage;
    }

    public void SpawnErrorDialog(string errorMessage, Action closeAction)
    {
        Canvas gui = FindGUICanvas();

        GameObject errorDialog = Instantiate(errorDialogPrefab);
        errorDialog.transform.SetParent(gui.transform, false);
        errorDialog.GetComponent<ErrorDialog>().errorMessage = errorMessage;
        errorDialog.GetComponent<ErrorDialog>().closeAction = closeAction;
    }

    public void SpawnConfirmationDialog(string confirmationMessage)
    {
        Canvas gui = FindGUICanvas();

        GameObject confirmationDialog = Instantiate(confirmationDialogPrefab);
        confirmationDialog.transform.SetParent(gui.transform, false);
        confirmationDialog.GetComponent<ConfirmationDialog>().confirmationMessage = confirmationMessage;
    }

    public void SpawnConfirmationDialog(string confirmationMessage, Action acceptAction, Action declineAction)
    {
        Canvas gui = FindGUICanvas();

        GameObject confirmationDialog = Instantiate(confirmationDialogPrefab);
        confirmationDialog.transform.SetParent(gui.transform, false);
        confirmationDialog.GetComponent<ConfirmationDialog>().confirmationMessage = confirmationMessage;
        confirmationDialog.GetComponent<ConfirmationDialog>().acceptAction = acceptAction;
        confirmationDialog.GetComponent<ConfirmationDialog>().declineAction = declineAction;
    }

    private  Canvas FindGUICanvas()
    {
        return GameObject.FindObjectOfType<Canvas>();
    }
}
