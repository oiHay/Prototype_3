using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    
    void Update()
    {
        transform.Translate(Vector3.left * (Time.deltaTime * _moveSpeed));
    }
}
