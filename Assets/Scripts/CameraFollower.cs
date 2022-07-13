using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] [Range(0.01f, 0.7f)]
    private float _smoothness = 0.3f;
    
    [SerializeField] private Transform _target;
    
    private Vector3 _positionOffset;

    private void Awake()
    {
        _positionOffset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        var desiredPosition = _target.position + _positionOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 1f / _smoothness * Time.deltaTime);
    }
}