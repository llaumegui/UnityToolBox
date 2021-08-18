using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TPSController : MonoBehaviour
{
    [Header("Data")]
    public float Speed = 10f;
    public float CameraSensitivity = 5f;
    public float ViewLock = 75f;

    Rigidbody _rb;
    [SerializeField]Transform _cameraPivot;

    float _xRot;

    [Header("Debug")]
    public bool EnableFreeMove;
    bool _freeMove;

    float _timeDoublePress = .5f;
    float _firstPress;
    float _secondPress;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckOtherInputs();
    }

    void FixedUpdate()
    {
        UpdateVelocity();
        UpdateCamera();
    }

    private void CheckOtherInputs()
    {
        if(EnableFreeMove)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (_firstPress == 0)
                {
                    _firstPress = Time.time;
                    StartCoroutine(ResetDoublePress());
                }
                else
                {
                    _secondPress = Time.time;

                    _freeMove = _secondPress - _firstPress <= _timeDoublePress ? !_freeMove : _freeMove;
                    _firstPress = 0;
                    _secondPress = 0;
                }
            }
        }
    }

    IEnumerator ResetDoublePress()
    {
        yield return new WaitForSeconds(_timeDoublePress*2);
        _firstPress = 0;
        _secondPress = 0;
    }

    private void UpdateCamera()
    {
        float x = Input.GetAxis("HorizontalRightStick");
        float y = Input.GetAxis("VerticalRightStick");

        _xRot -= (y * CameraSensitivity);
        _xRot = Mathf.Clamp(_xRot, -ViewLock, ViewLock);

        transform.Rotate(Vector3.up * x * CameraSensitivity);
        _cameraPivot.localRotation = Quaternion.Euler(_xRot, 0, 0);
    }

    void UpdateVelocity()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");


        Vector3 workspace = (transform.right * x + transform.forward * y) * Speed;
        if (!_freeMove)
            workspace.y = _rb.velocity.y;

        _rb.velocity = workspace;

        if (Input.GetButton("Jump") && _freeMove)
        {
            _rb.velocity = _rb.velocity + (Vector3.up * Speed / 2);
        }
    }
}
