#if UNITY_WEBGL
using UnityEngine;
using System.Runtime.InteropServices;

public class ExternalLink : MonoBehaviour
{
	public string urlToOpen = "http://unity3d.com";

	public void OpenLinkJSPlugin()
	{
        print("Trying to use OpenLinkJSPlugin");
		openWindow(urlToOpen);
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);
}
#endif