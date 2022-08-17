using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class AreaManager : MonoBehaviour
{
    [SerializeField]
	GameObject player;

	[SerializeField]
	GameObject PickUpAreaPrefab, DropAreaPrefab, EscapeAreaPrefab;

	public float spawnScale = 20f;

	[SerializeField]
	float spawnRange = 100f;

    [NonSerialized]
    public PickUpArea PickUpArea;
    [NonSerialized]
    public DropArea[] DropAreas;
    [NonSerialized]
    public EscapeArea EscapeArea;

    AbstractMap map;

	void Start()
	{
		map = gameObject.GetComponent<AbstractMap>();
        map.OnUpdated += UpdateAreasPosition;
		//Load();
	}

    void Update()
    {
        PickUpArea = FindObjectOfType<PickUpArea>();
        DropAreas = FindObjectsOfType<DropArea>();
        EscapeArea = FindObjectOfType<EscapeArea>();
    }

    public void RegenerateZones()
    {
        if (PickUpArea)
        {
            SpawnPickUpArea(PickUpArea.drugsCount);
            DestroyImmediate(PickUpArea.gameObject);
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

    void UpdateAreasPosition()
    {
        var areas = FindObjectsOfType<Area>();
        foreach (Area area in areas)
        {
            area.gameObject.transform.position = map.GeoToWorldPosition(area.location) + new Vector3(0, 1, 0);
        }
    }

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
        SpawnArea(map.WorldToGeoPosition(player.transform.position), EscapeAreaPrefab);//Проверить
    }

    Vector2d GenerateRandomLocation()
    {
		return map.WorldToGeoPosition(player.transform.position + new Vector3(UnityEngine.Random.Range(-spawnRange, spawnRange), 0, UnityEngine.Random.Range(-spawnRange, spawnRange)) * map.transform.lossyScale.x);
	}

    GameObject SpawnArea(Vector2d location, GameObject area)
    {
        var instance = Instantiate(area);
        instance.GetComponent<Area>().location = location;
        instance.transform.localPosition = map.GeoToWorldPosition(location, true) + new Vector3(0, 1, 0);
        instance.transform.localScale = new Vector3(spawnScale * map.transform.localScale.x, spawnScale * map.transform.localScale.y, spawnScale * map.transform.localScale.z);
        instance.transform.SetParent(map.transform);
        return instance;
    }

    public float GetRadius(GameObject area)
    {
        SpriteRenderer checkObject = area.transform.GetChild(0).GetComponent<SpriteRenderer>();
        float textureRadius = checkObject.sprite.textureRect.width * checkObject.gameObject.transform.localScale.x / 4;
        float density = area.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        float radius = (textureRadius / density) * spawnScale * map.transform.localScale.x;
        //print(radius);
        return radius;
    }
}