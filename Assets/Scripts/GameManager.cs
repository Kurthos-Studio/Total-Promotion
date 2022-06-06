using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DataHandler.LoadGameData();

        if (!SceneManager.GetSceneByName("UI").isLoaded)
        {
            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        }
    }
}
