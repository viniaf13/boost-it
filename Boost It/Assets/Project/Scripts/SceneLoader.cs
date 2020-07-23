using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float nextLevelDelay = 2f;
    private int currentBuildIndex = 0;

    private void Awake()
    {
        if (FindObjectsOfType<SceneLoader>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(currentBuildIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(currentBuildIndex+1);
        currentBuildIndex++;
    }

    private IEnumerator LoadSceneWithDelay(int sceneIndex)
    {
        yield return new WaitForSeconds(nextLevelDelay);
        SceneManager.LoadScene(sceneIndex);
    }
}
