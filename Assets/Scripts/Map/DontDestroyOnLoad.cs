using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad Game;
    void Start()
    {
        if (Game != null)
        {
            Game.gameObject.SetActive(true);
            Destroy(gameObject);
            return;
        }
        Game = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Map"))
        {
            Game.gameObject.SetActive(false);
        }
    }
}