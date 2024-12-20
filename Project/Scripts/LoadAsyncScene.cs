using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAsyncScene : MonoBehaviour
{
    public GameObject[] unloadObjects;
    
    public void LoadSceneAsync(int sceneIndex)
    {
        // this function loads in a scene additively to the currently loaded scene
        StartCoroutine(LoadYourAsyncScene(sceneIndex));
    }
    
    IEnumerator LoadYourAsyncScene(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            foreach (GameObject obj in unloadObjects)
            {
                obj.SetActive(false);
            }
            yield return null;
        }
    }

}
