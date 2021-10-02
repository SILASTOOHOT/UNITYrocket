
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1.5f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
       
    
    void OnCollisionEnter(Collision other)

    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSucessSequence();
                break;
            case "Fuel":
                Debug.Log("Fuel has been added");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

     void StartSucessSequence()
     {
        isTransitioning = true;
        audioSource.PlayOneShot(success);
        
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
     }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(crash);
        
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);

    }


    void LoadNextLevel()
    {
        int currectSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currectSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currectSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currectSceneIndex);
    }
}