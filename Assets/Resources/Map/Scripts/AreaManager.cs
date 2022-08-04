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
        SpawnArea(new Vector2d(player.transform.position.x, player.transform.position.y), EscapeAreaPrefab);//Проверить
    }

    Vector2d GenerateRandomLocation()
    {
		return map.WorldToGeoPosition(player.transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)) * map.transform.lossyScale.x);
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
