using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class SaveGame
{
    public static IEnumerator Save(Board board)
    {
        GameData state = new GameData(board);
        var requestBody = JsonUtility.ToJson(state);

        UnityWebRequest www = UnityWebRequest.Put("https://1xm79yv3i9.execute-api.us-west-2.amazonaws.com/prod/state", requestBody);
        www.method = "POST";
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("didn't see an error");
        }
    }
}
