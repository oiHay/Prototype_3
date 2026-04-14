using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10.0f;
    [SerializeField] private float _gravityModifier = 1.0f;
    [SerializeField] private bool _isGrounded;

    // private bool _isGameOver = false;
    private Rigidbody _rb;

    public static event Action OnGameOver;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Physics.gravity *= _gravityModifier; //  gravity = gravity * _gravityModifier
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded) //Verifica se o player apertou espaço e se o personagem está no chão
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false; // Por estar no ar, a boolean torna-se falsa, impedindo que o if statement seja lido
        }
    }

    private void OnCollisionEnter(Collision collision) //Se o player estiver colidindo com algo
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true; // A boolean torna-se verdadeira
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            // _isGameOver = true;
            OnGameOver?.Invoke();
        }
    }
}
