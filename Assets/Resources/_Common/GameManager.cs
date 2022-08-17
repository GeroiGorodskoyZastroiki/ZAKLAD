using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mapbox.Unity.Map;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float catchRate;
            
    Player player;
    MapUI mapUI;
    AreaManager areaManager;
    Camera cam;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            Initilize();
            DontDestroyOnLoad(gameObject);
            mapUI.SplashCompany();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    void Initilize()
    {
        player = FindObjectOfType<Player>();
        mapUI = FindObjectOfType<MapUI>();
        areaManager = FindObjectOfType<AreaManager>();
        cam = FindObjectOfType<Camera>();
        SceneManager.sceneLoaded += LoadScene;
        SceneManager.sceneUnloaded += UnloadScene;
    }

    void LoadScene(Scene scene, LoadSceneMode loadmode)
    {
        if (scene.name == "Camera")
        {
            cam.gameObject.SetActive(false);
            mapUI.gameObject.SetActive(false);
            areaManager.gameObject.SetActive(false);
            player.gameObject.transform.parent.gameObject.SetActive(false);
        }
        if (scene.name == "Map")
        {
            Initilize();
        }
    }

    void UnloadScene(Scene scene)
    {
        if (scene.name == "Camera")
        {
            cam.gameObject.SetActive(true);
            mapUI.gameObject.SetActive(true);
            areaManager.gameObject.SetActive(true);
            player.gameObject.transform.parent.gameObject.SetActive(true);
            DestroyImmediate(player.inArea.gameObject);
            player.drugsStock--;
            player.drugsStats++;
            if (Random.Range(0f, 1f) < System.Math.Clamp(catchRate, 0, 1))
            {
                areaManager.SpawnEscapeArea();
                mapUI.ShowNotification(mapUI.pursuitNotification);
                StartCoroutine(Pursuit());
            }
        }
    }

    IEnumerator Pursuit()
    {
        while (!areaManager.EscapeArea)
        {
            yield return new WaitForSeconds(0.1f);
        }
        mapUI.timer.gameObject.transform.parent.gameObject.SetActive(true);
        for (int i = areaManager.EscapeArea.timeToEscape; i > 0; i--)
        {
            if (Vector3.Distance(areaManager.EscapeArea.gameObject.transform.position, player.transform.position) >= areaManager.GetRadius(areaManager.EscapeArea.gameObject))
            {
                EndPursuit();
                DestroyImmediate(areaManager.EscapeArea.gameObject);
                yield break;
            }
            yield return new WaitForSeconds(1);
            mapUI.timer.text = i.ToString();
        }
        EndPursuit();
        mapUI.SplashEndGame();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Map");
    }

    void EndPursuit()
    {
        mapUI.ForceHideNotification();
        mapUI.timer.gameObject.transform.parent.gameObject.SetActive(false);
        mapUI.timer.text = "--";
    }
}
