using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mapbox.Unity.Map;

public class GameManager : MonoBehaviour
{
    public float caughtRate;
            
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
        areaManager.gameObject.SetActive(false);
        player.gameObject.transform.parent.gameObject.SetActive(false);
    }

    void UnloadCamera(Scene scene)
    {
        mapUI.gameObject.SetActive(true);
        areaManager.gameObject.SetActive(true);
        player.gameObject.transform.parent.gameObject.SetActive(true);
        DestroyImmediate(player.inArea.gameObject);
        player.drugsStock--;
        player.drugsStats++;
        if (Random.Range(0f, 1f) < System.Math.Clamp(caughtRate, 0 ,1))
        {
            areaManager.SpawnEscapeArea();
            mapUI.ShowNotification();
            StartCoroutine(Pursuit());
        }
    }

    IEnumerator Pursuit()
    {
        while (!areaManager.EscapeArea)
        {
            yield return new WaitForSeconds(0.1f);
        }
        for (int i = areaManager.EscapeArea.timeToEscape; i > 0; i--)
        {
            if (Vector3.Distance(areaManager.EscapeArea.gameObject.transform.position, player.transform.position) >= areaManager.GetRadius(areaManager.EscapeArea.gameObject))
            {
                print("niceEscape");
                mapUI.HideNotification();
                DestroyImmediate(areaManager.EscapeArea.gameObject);
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
        mapUI.HideNotification();
        print("Cauth");
        //сплеш концовка
    }
}
