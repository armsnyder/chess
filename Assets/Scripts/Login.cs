using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (String.IsNullOrEmpty(GetQueryString()))
        {
            OpenURL("https://auth.superchess.net/login?response_type=code&client_id=225lnu7q1oosrji56o8hh2k35b&redirect_uri=https://superchess.net");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void OpenURL(string url);

    [DllImport("__Internal")]
    private static extern string GetQueryString();

#else
    private static void OpenURL(string url) { }
    private static string GetQueryString() { return "doot"; }

#endif
}
