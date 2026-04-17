using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [Header("Gravidade")]
    [SerializeField] private float floatForce;
    [SerializeField] private float gravityModifier = 1.5f;
    [SerializeField] private float _topBounder;
    [SerializeField] private float _bottomBounder;

    [Header("Áudio")]
    [SerializeField] private AudioClip moneySound;
    [SerializeField] private AudioClip explodeSound;
    [SerializeField] private AudioClip bounceSound;
    
    [Header("Partículas")]
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem fireworksParticle;
    
    private Rigidbody playerRb;
    private AudioSource playerAudio;
    [HideInInspector] public bool gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        if(playerRb != null)
        {
            Physics.gravity *= gravityModifier;
            playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse); // Apply a small upward force at the start of the game
        }
    }

    // Update is called once per frame
    void Update()
    {
        float campledY = Mathf.Clamp(transform.position.y, _bottomBounder, _topBounder);
        transform.position = new Vector3(transform.position.x, campledY, transform.position.z);
        
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * (Time.deltaTime * floatForce), ForceMode.Impulse);
        }
        
        if (campledY<= _bottomBounder)
        {
            playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse); // Apply a small upward force at the start of the game
            playerAudio.PlayOneShot(bounceSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            HandleExplosion();
            gameOver = true;
            Destroy(other.gameObject);
            Destroy(gameObject);
        } 
    
        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            HandleMoney();
            Destroy(other.gameObject);
        }
    }

    private void HandleExplosion()
    {
        explosionParticle.Play();
        playerAudio.PlayOneShot(explodeSound, 1.0f);
    }

    private void HandleMoney()
    {
        fireworksParticle.Play();
        playerAudio.PlayOneShot(moneySound, 1.0f);
    }
}
