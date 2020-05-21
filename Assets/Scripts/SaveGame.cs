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
            Debug.Log("Game was saved.");
        }
    }

    public static IEnumerator Load(Board board)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://1xm79yv3i9.execute-api.us-west-2.amazonaws.com/prod/state");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var response = www.downloadHandler.text;

            GameData state = JsonUtility.FromJson<GameData>(response);

            board.ClearPieces();

            board.whoseTurn = state.whoseTurn;
            foreach (PieceData p in state.pieces)
            {
                board.CreateAndPlacePiece(p.pieceType, p.team, board.GetCell(p.x, p.y), p.hasMoved);
            }
        }
    }
}
