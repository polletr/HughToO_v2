using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField,Tooltip("What You Want The Camera To Follow")]
    private GameObject _followTarget;
    [SerializeField]
    private float _moveSpeed = 2f;
    [SerializeField]
    private Vector3 _followOffset;

    private Vector3 _currentVelocity;
    private static CameraFollow _instance;

    public static CameraFollow Get() => _instance;

    void Awake()
    {
        _instance = this;
    }

    void LateUpdate()
    {
        if (_followTarget != null)
        {
            Vector3 target_pos = _followTarget.transform.position + _followOffset;
            transform.position = Vector3.SmoothDamp(transform.position, target_pos, ref _currentVelocity, 1f / _moveSpeed);
        }
    }

   
}
