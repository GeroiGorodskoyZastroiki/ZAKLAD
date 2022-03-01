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
	AbstractMap map;

	[SerializeField]
	GameObject player;

	[SerializeField]
	InputField order;

	[SerializeField]
	GameObject area;

	[SerializeField]
	float spawnScale = 100f;

	Vector2d[] locations;

    public void Update()
    {
		int areaCount = int.Parse(order.text);
	}

    public void SpawnArea(string areaType, int areaCount, int drugCount)
	{
		locations = new Vector2d[areaCount];
		for (int i = 0; i < areaCount; i++)
		{
			locations[i] = map.WorldToGeoPosition(player.transform.position + new Vector3(Random.Range(-100,100), 0, Random.Range(-100, 100)));
			var instance = Instantiate(area);
			instance.GetComponent<Area>().location = locations[i];
			instance.transform.localPosition = map.GeoToWorldPosition(locations[i], true) + new Vector3(0, 1, 0);
			instance.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
			instance.transform.SetParent(map.transform);
			instance.GetComponent<Area>().areaType = areaType;
		}
	}
}
