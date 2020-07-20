using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotationSensitivity = 100f;
    [SerializeField] float thrustForce = 20f;

    private PlayerControls controls;
    private float direction;
    private Rigidbody rigidBody;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Thrust.performed += _ => Thrust();

        controls.Gameplay.Rotate.performed += ctx => direction = ctx.ReadValue<float>();
        controls.Gameplay.Rotate.canceled += ctx => direction = 0f;
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Rotate();
    }
    private void Thrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustForce);
    }
    private void Rotate()
    {
        Vector3 rotation = new Vector3(0, 0, rotationSensitivity * direction) * Time.deltaTime;
        transform.Rotate(rotation);
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
