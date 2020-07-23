using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Vector3 obstacleMovement = new Vector3(10f, 0, 0);
    [SerializeField] float period = 0f;

    private Vector3 startPos;
    private float movementFactor;
    private const float tau = Mathf.PI * 2f;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period;

        movementFactor = Mathf.Sin(cycles * tau) * 0.5f + 0.5f ; //Range 0 to 1
        Vector3 offset = movementFactor * obstacleMovement;
        transform.position = startPos + offset;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            player.TakeHit();
        }
    }
}
