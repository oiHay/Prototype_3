using System;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameObject _obstaclePrefab; // Referencia o gameObject que será instanciado
    
    [Header("Spawn Values")]
    [SerializeField] private Vector3 _spawnPos = new Vector3(20,0,0);  // Determina a posição do spawn do objeto

    [SerializeField] private float _startDelay = 2.0f;
    [SerializeField] private float _repeatRate = 2.0f;

    [Header("Private")] 
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

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), _startDelay, _repeatRate);
    }

    private void SpawnObstacle()
    {
        if (!_isGameOver)
        {
            Instantiate(_obstaclePrefab, _spawnPos, _obstaclePrefab.transform.rotation); // Cria o objeto espeçifico na posição desejada com a rotação original do objeto
        }
    }
}
