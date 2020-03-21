using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorDialog : MonoBehaviour
{
    public string errorMessage;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Text>().text = errorMessage;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCloseButtonPushed()
    {
        Destroy(this.gameObject);
    }
}
