using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public Player player;
    public MapUI mapUI;
    public AreaManager areaManager;
    void Start()
    {
        SceneManager.sceneLoaded += LoadCamera;
        SceneManager.sceneUnloaded += UnloadCamera;
    }

    void LoadCamera(Scene scene, LoadSceneMode loadmode)
    {
        mapUI.gameObject.SetActive(false);
        areaManager.gameObject.transform.root.gameObject.SetActive(false);
    }

    void UnloadCamera(Scene scene)
    {
        mapUI.gameObject.SetActive(true);
        areaManager.gameObject.transform.root.gameObject.SetActive(true);
        DestroyImmediate(player.inArea.gameObject);
        player.drugsStock--;
        player.drugsStats++;
        if (Math.Round(UnityEngine.Random.Range(0f, 1f)) == 1)
        {
            //
        }
    }
}
