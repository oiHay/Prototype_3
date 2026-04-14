using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 _startPos;
    private float _offSet;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = transform.position;
        _offSet = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < _startPos.x - _offSet)
        {
            transform.position = _startPos;
        }
    }
}
