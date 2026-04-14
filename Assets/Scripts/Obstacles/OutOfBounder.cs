using System;
using UnityEngine;

public class OutOfBounder : MonoBehaviour
{
    [SerializeField] private float _leftBound;

    private void Update()
    {
        if (transform.position.x <= -_leftBound)
        {
            Destroy(gameObject);
        }
    }
}