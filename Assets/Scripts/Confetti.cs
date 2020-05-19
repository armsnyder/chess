using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    [SerializeField]
    GameObject _confettPrefab = null;

    void OnGameOver(bool team)
    {
        Instantiate<GameObject>(_confettPrefab, transform);
    }

    void OnEnable()
    {
        Board.GameOver += OnGameOver;
    }

    void OnDisable()
    {
        Board.GameOver -= OnGameOver;
    }
}
