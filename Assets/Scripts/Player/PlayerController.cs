using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private ParticleSystem _collisionParticle;
    [SerializeField] private ParticleSystem _runParticle;

    [Header("Sound")] 
    [SerializeField] private AudioClip _jumpClip;
    [SerializeField] private AudioClip _collisionClip;

    [Header("Configurações do Pulo")]
    [SerializeField] private float _jumpForce = 10.0f;
    [SerializeField] private float _gravityModifier = 1.0f;
    
    [Header("Variáveis Internas  ")] 
    private Animator _anim;
    private Rigidbody _rb;
    private AudioSource _source;
    private bool _isGrounded;
    private bool _isGameOver;

    public static event Action OnGameOver;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _source = GetComponent<AudioSource>();
        
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
        _source.PlayOneShot(_jumpClip, 1.0f);
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
            _source.PlayOneShot(_collisionClip, 1.0f);
            OnGameOver?.Invoke();
        }
    }
}
