using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _xVelocity;
    private float _zVelocity;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask layerMask;
    
    private Rigidbody _rigidbody;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveSphere();
    }

    private void MoveSphere()
    {
        _xVelocity = Input.GetAxis("Horizontal") * speed;
        _zVelocity = Input.GetAxis("Vertical") * speed;

        _rigidbody.velocity = new Vector3(_xVelocity, _rigidbody.velocity.y, _zVelocity);
    }

    private void OnCollisionExit(Collision other)
    {
        Invoke(nameof(CheckPlayerState), 2);
    }

    private void CheckPlayerState()
    {
        var store = new Collider[2]; 
        if (Physics.OverlapSphereNonAlloc(transform.position, 1, store, layerMask) > 0) return;
        
        GameController.Singleton.OpenResults(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        GameController.Singleton.OpenResults(true);
    }
}
