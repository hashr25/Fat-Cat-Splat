using UnityEngine;
using System.Runtime.InteropServices;

public class ExternalLink : MonoBehaviour
{
	public string urlToOpen = "http://unity3d.com";

	public void OpenLinkJSPlugin()
	{
#if !UNITY_EDITOR
        print("Trying to use OpenLinkJSPlugin");
		openWindow(urlToOpen);
#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);
}