using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class AreaSpawner : MonoBehaviour
{
    [SerializeField]
	GameObject player;

	[SerializeField]
	GameObject area;

	[SerializeField]
	public float spawnScale = 20f;

	[SerializeField]
	float spawnRange = 100f;

	AbstractMap map;
	public int drugs;

	public void Start()
	{
		map = gameObject.GetComponent<AbstractMap>();
		Load();
	}

    private void Load()
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

	public void SpawnAreas(int areaCount, string areaType, int drugsCount)
    {
		for (int i = 0; i < areaCount; i++)
        {
			LoadArea(GenerateLocation(), areaType, drugsCount);
        }
    }

	public Vector2d GenerateLocation()
    {
		return map.WorldToGeoPosition(player.transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)));
	}

    public void LoadArea(Vector2d location, string areaType, int drugsCount)
    {
		var instance = Instantiate(area);
		instance.transform.localPosition = map.GeoToWorldPosition(location, true) + new Vector3(0, 0.1f, 0);
		instance.transform.localScale = new Vector3(spawnScale * map.transform.localScale.x, spawnScale * map.transform.localScale.y, spawnScale * map.transform.localScale.z);
		instance.transform.SetParent(map.transform);
		instance.GetComponent<Area>().map = map;
		instance.GetComponent<Area>().location = location;
		instance.GetComponent<Area>().areaType = areaType;
		instance.GetComponent<Area>().drugsCount = drugsCount;
    }
}
