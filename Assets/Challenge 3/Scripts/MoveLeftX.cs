using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftX : MonoBehaviour
{
    public float speed;
    private float leftBound = -10;
    
    private bool _gameOver;

    void OnEnable()
    {
        PlayerControllerX.OnGameOver += HandleGameOver;
    }

    void OnDisable()
    {
        PlayerControllerX.OnGameOver -= HandleGameOver;
    }

    private void HandleGameOver()
    {
        _gameOver = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        // If game is not over, move to the left
        if (!_gameOver)
        {
            transform.Translate(Vector3.left * (speed * Time.deltaTime), Space.World);
        }

        // If object goes off screen that is NOT the background, destroy it
        if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
        {
            Destroy(gameObject);
        }

    }
}
