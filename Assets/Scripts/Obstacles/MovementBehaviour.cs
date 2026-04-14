using System;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private bool _isGameOver = false;

    private void OnEnable()
    {
        PlayerController.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        PlayerController.OnGameOver -= HandleGameOver;
    }

    private void HandleGameOver()
    {
        _isGameOver = true;
    }

    void Update()
    {
        if (!_isGameOver)
        {
            transform.Translate(Vector3.left * (Time.deltaTime * _moveSpeed));
        }
    }
}
