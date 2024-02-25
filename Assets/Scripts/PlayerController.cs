using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Gravity = 9.8f;
    public float JumpForce;
    public float Speed;

    private float _fallVelocity = 0;
    private Vector3 _moveVector;

    private CharacterController _characterController;

    private void Start()
    {
        InitComponentLinks();
    }

    private void Update()
    {
        MoveUpdate();
    }

    void FixedUpdate()
    {
        MoveFixedUpdate();
    }

    private void MoveFixedUpdate()
    {
        _characterController.Move(_moveVector * Time.fixedDeltaTime * Speed);

        _fallVelocity += Gravity * Time.fixedDeltaTime;
        _characterController.Move(Vector3.down * Time.fixedDeltaTime * _fallVelocity);

        if (_characterController.isGrounded)
        {
            _fallVelocity = 0;
        }
    }

    private void MoveUpdate()
    {
        _moveVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _moveVector += transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _moveVector += transform.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _moveVector -= transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _moveVector -= transform.forward;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
        {
            _fallVelocity = -JumpForce;
        }
    }

    private void InitComponentLinks()
    {
        _characterController = GetComponent<CharacterController>();
    }
}
