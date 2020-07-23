using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float rotationSensitivity = 100f;
    [SerializeField] float thrustForce = 1000f;

    private PlayerControls controls;
    private bool isThrusting =false;
    private float direction;
    private Rigidbody rigidBody;
    private AudioSource audioSource;


    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Thrust.performed += ctx => isThrusting = ctx.ReadValueAsButton();
        controls.Gameplay.Thrust.canceled += ctx => isThrusting = ctx.ReadValueAsButton();

        controls.Gameplay.Rotate.performed += ctx => direction = -(ctx.ReadValue<float>());
        controls.Gameplay.Rotate.canceled += _ => direction = 0f;
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        Thrust();
        Rotate();
    }
    private void Thrust()
    {
        if (isThrusting)
        {
            if (!audioSource.isPlaying) audioSource.Play();
            rigidBody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;
        Vector3 rotationVector = Vector3.forward * rotationSensitivity * direction;
        transform.Rotate(rotationVector * Time.deltaTime);
        rigidBody.freezeRotation = false;
    }

    public void TakeHit()
    {
        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
        if (!sceneLoader) Debug.LogError("SceneLoader not found.");

        sceneLoader.RestartLevel();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
