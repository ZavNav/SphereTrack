using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Transform _playerTransform;
    [SerializeField] private float zOffset;
    [SerializeField] private float yOffset;
    private Vector3 _offset;
    void Start()
    {
        _playerTransform = FindObjectOfType<Player>().transform;
        _offset = new Vector3(0, yOffset, zOffset);
    }

    void LateUpdate()
    {
        transform.position = _playerTransform.position + _offset;
        transform.LookAt(_playerTransform);
    }
}
