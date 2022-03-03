using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;

public class AreaSpawner : MonoBehaviour
{
    [SerializeField]
	GameObject player;

	[SerializeField]
	GameObject area;

	[SerializeField]
	float spawnScale = 20f;

	[SerializeField]
	float spawnRange = 100f;

	AbstractMap map;
	Vector2d[] locations;
	public int drugs;

	public void Start()
	{
		map = gameObject.GetComponent<AbstractMap>();
	}

    public void SpawnArea(string areaType, int areaCount, int drugsCount)
	{
		locations = new Vector2d[areaCount];
		for (int i = 0; i < areaCount; i++)
		{
			locations[i] = map.WorldToGeoPosition(player.transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)));
			var instance = Instantiate(area);
			instance.GetComponent<Area>().location = locations[i];
			instance.transform.localPosition = map.GeoToWorldPosition(locations[i], true) + new Vector3(0, 1, 0);
			instance.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
			instance.transform.SetParent(map.transform);
			instance.GetComponent<Area>().spawnScale = spawnScale;
			instance.GetComponent<Area>().areaType = areaType;
			instance.GetComponent<Area>().drugsCount = drugsCount;
		}
	}
}
