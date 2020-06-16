using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    static string _accessToken;
    public static string accessToken { get { return _accessToken; } }
    static string _idToken;
    public static string idToken { get { return _idToken; } }

    // Start is called before the first frame update
    void Start()
    {
        var code = GetQueryString();
        if (String.IsNullOrEmpty(code))
        {
            OpenURL("https://auth.superchess.net/login?response_type=code&client_id=225lnu7q1oosrji56o8hh2k35b&redirect_uri=https://superchess.net");
        }
        else
        {
            StartCoroutine(GetToken(code));
        }
    }

    IEnumerator GetToken(string code)
    {
        WWWForm form = new WWWForm();
        form.AddField("grant_type", "authorization_code");
        form.AddField("code", code);
        form.AddField("client_id", "225lnu7q1oosrji56o8hh2k35b");
        form.AddField("redirect_uri", "https://superchess.net");
        UnityWebRequest www = UnityWebRequest.Post("https://auth.superchess.net/oauth2/token", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var response = www.downloadHandler.text;
            Debug.Log(response);

            var responseObject = JsonUtility.FromJson<TokenResponse>(response);
            _idToken = responseObject.id_token;
            _accessToken = responseObject.access_token;
            Debug.Log(_idToken);
            Debug.Log(_accessToken);
        }
        // SceneManager.LoadScene("SampleScene");
    }

    class TokenResponse
    {
        public string id_token;
        public string access_token;
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
