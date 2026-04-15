using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Reference")] 
    [SerializeField] private Animator _anim;
    [SerializeField] private ParticleSystem _collisionParticle;
    [SerializeField] private ParticleSystem _runParticle;

    [Header("Jump Parameters")]
    [SerializeField] private float _jumpForce = 10.0f;
    [SerializeField] private float _gravityModifier = 1.0f;
    [SerializeField] private bool _isGrounded;

    private bool _isGameOver;
    private Rigidbody _rb;

    public static event Action OnGameOver;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        
        Physics.gravity *= _gravityModifier; //  gravity = gravity * _gravityModifier
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !_isGameOver) //Verifica se o player apertou espaço e se o personagem está no chão
        {
            OnJump();
        }
    }

    private void OnJump()
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isGrounded = false; // Por estar no ar, a boolean torna-se falsa, impedindo que o if statement seja lido
        _anim.SetTrigger("Jump_trig");
        _runParticle.Stop();
    }

    private void OnCollisionEnter(Collision collision) //Se o player estiver colidindo com algo
    {
        if (collision.gameObject.CompareTag("Ground") && !_isGameOver)
        {
            _isGrounded = true; // A boolean torna-se verdadeira
            _runParticle.Play();
        }
        else
        {
            _runParticle.Stop();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            _isGameOver = true;
            _anim.SetBool("Death_b", true);
            _anim.SetInteger("DeathType_int", 1);
            _collisionParticle.Play();
            OnGameOver?.Invoke();
        }
    }
}
