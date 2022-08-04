using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class AreaManager : MonoBehaviour
{
    [SerializeField]
	GameObject player;

	[SerializeField]
	GameObject PickUpAreaPrefab, DropAreaPrefab, EscapeAreaPrefab;

    [SerializeField]
	public float spawnScale = 20f;

	[SerializeField]
	float spawnRange = 100f;

    public PickUpArea PickUpArea;
    public DropArea[] DropAreas;
    public EscapeArea EscapeArea;
    AbstractMap map;

	void Start()
	{
		map = gameObject.GetComponent<AbstractMap>();
		//Load();
	}

    void Update()
    {
        PickUpArea = FindObjectOfType<PickUpArea>();
        DropAreas = FindObjectsOfType<DropArea>();
        EscapeArea = FindObjectOfType<EscapeArea>();
    }

    public void RegenerateZones() //ÔÓ‚ÂËÚ¸
    {
        if (PickUpArea)
        {
            SpawnPickUpArea(PickUpArea.drugsCount);
            DestroyImmediate(PickUpArea);
            return;
        }
        if (DropAreas.Length > 0)
        {
            SpawnDropAreas(DropAreas.Length);
            foreach (DropArea area in DropAreas)
            {
                DestroyImmediate(area.gameObject);
            }
        }
    }

    //void Update() //Õ¿œ»—¿“‹ Œ¡ÕŒ¬À≈Õ»≈ “–¿Õ—‘Œ–Ã¿ ƒÀﬂ  ¿∆ƒŒ… «ŒÕ€
    //{
    //	if (map.transform.localScale.x < 0.35)
    //	{
    //		gameObject.transform.GetChild(0).gameObject.SetActive(false);
    //		gameObject.transform.GetChild(1).gameObject.SetActive(false);
    //		//area.transform.GetChild(2).gameObject.SetActive(true);
    //	}
    //	else
    //	{
    //		gameObject.transform.GetChild(0).gameObject.SetActive(true);
    //		gameObject.transform.GetChild(1).gameObject.SetActive(true);
    //		gameObject.transform.GetChild(2).gameObject.SetActive(false);
    //	}

    //	if (areaType == "PickUp")
    //	{
    //		gameObject.transform.GetChild(1).gameObject.SetActive(false);
    //	}
    //	else
    //	{
    //		gameObject.transform.GetChild(0).gameObject.SetActive(false);
    //	}
    //}

    void Load()
    {
		while (true)
        {
			if (map.isActiveAndEnabled)
			{
				SaveManager.Instance.Load();
				return;
			}
		}
	}

    public void SpawnDropAreas(int areaCount)
    {
        for (int i = 0; i < areaCount; i++)
        {
            SpawnArea(GenerateRandomLocation(), DropAreaPrefab);
        }
        if (PickUpArea)
        {
            DestroyImmediate(PickUpArea.gameObject);
        }
    }

    public void SpawnPickUpArea(int drugsCount)
    {
        SpawnArea(GenerateRandomLocation(), PickUpAreaPrefab).GetComponent<PickUpArea>().drugsCount = drugsCount;
    }

    public void SpawnEscapeArea()
    {
        SpawnArea(new Vector2d(player.transform.position.x, player.transform.position.y), EscapeAreaPrefab);//œÓ‚ÂËÚ¸
    }

    Vector2d GenerateRandomLocation()
    {
		return map.WorldToGeoPosition(player.transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)));
	}

    GameObject SpawnArea(Vector2d location, GameObject area)
    {
        var instance = Instantiate(area);
        instance.transform.localPosition = map.GeoToWorldPosition(location, true) + new Vector3(0, 0.1f, 0);
        instance.transform.localScale = new Vector3(spawnScale * map.transform.localScale.x, spawnScale * map.transform.localScale.y, spawnScale * map.transform.localScale.z);
        instance.transform.SetParent(map.transform);
        return instance;
    }
}
