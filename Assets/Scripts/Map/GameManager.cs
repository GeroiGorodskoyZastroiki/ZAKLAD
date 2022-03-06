using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public struct sArea
    {
        public Vector2d location;
        public string areaType;
        public int drugsCount;
    }

    public static List<sArea> areas;

    void Start()
    {
        if (gameManager != null)
        {
            Destroy(gameObject);
            return;
        }
        gameManager = this;
        DontDestroyOnLoad(gameObject);
        PlayerPrefs.
    }

    public static void SaveZones()
    {
        areas.Clear();
        Debug.Log("Cleared");
        Area[] areasArray = (Area[])FindObjectsOfType(typeof(Area));
        Debug.Log("Zones Collected");
        foreach (Area area in areasArray)
        {
            var area1 = new sArea { areaType = area.areaType, drugsCount = area.drugsCount, location = area.location };
            areas.Add(area1);
            Debug.Log(area1);
        }
        Debug.Log("Thats all");
    }

    private void Update()
    {

    }
}