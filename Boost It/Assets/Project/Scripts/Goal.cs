using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
            if (!sceneLoader) Debug.LogError("SceneLoader not found.");

            sceneLoader.LoadNextLevel();
        }
    }
}
