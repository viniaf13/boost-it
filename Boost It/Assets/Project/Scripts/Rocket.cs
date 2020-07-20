using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotationSensitivity = 100f;
    [SerializeField] float thrustForce = 1f;

    private PlayerControls controls;
    private bool isThrusting;
    private float direction;
    private Rigidbody rigidBody;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Thrust.performed += _ => isThrusting = true;
        controls.Gameplay.Thrust.canceled += _ => isThrusting = false;

        controls.Gameplay.Rotate.performed += ctx => direction = ctx.ReadValue<float>();
        controls.Gameplay.Rotate.canceled += ctx => direction = 0f;
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Thrust();
        Rotate();
    }
    private void Thrust()
    {
        if (!isThrusting) return;
        rigidBody.AddRelativeForce(Vector3.up * thrustForce);
    }
    private void Rotate()
    {
        Vector3 rotationVector = Vector3.forward * rotationSensitivity * direction;
        transform.Rotate(rotationVector * Time.deltaTime) ;
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
