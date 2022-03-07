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
	public float spawnScale = 20f;

	[SerializeField]
	float spawnRange = 100f;

	AbstractMap map;
	Vector2d[] locations;
	public int drugs;

	public void Start()
	{
		SaveManager.Instance.Load();
		map = gameObject.GetComponent<AbstractMap>();
	}

    public void SpawnArea(string areaType, int areaCount, int drugsCount)
	{
		locations = new Vector2d[areaCount];
		for (int i = 0; i < areaCount; i++)
		{
			var aboba = map.AbsoluteZoom; //����
			locations[i] = map.WorldToGeoPosition(player.transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)));
			var instance = Instantiate(area);
			instance.GetComponent<Area>().location = locations[i];
			instance.transform.localPosition = map.GeoToWorldPosition(locations[i], true) + new Vector3(0, 0.1f, 0);
			instance.transform.localScale = new Vector3(spawnScale * map.transform.localScale.x, spawnScale * map.transform.localScale.y, spawnScale * map.transform.localScale.z);
			instance.transform.SetParent(map.transform);
			instance.GetComponent<Area>().map = map;
			instance.GetComponent<Area>().areaType = areaType;
			instance.GetComponent<Area>().drugsCount = drugsCount;
		}
	}

    public void LoadArea(Vector2d location, string areaType, int drugsCount)
    {
		var instance = Instantiate(new Area());
		instance.transform.localPosition = map.GeoToWorldPosition(location, true) + new Vector3(0, 0.1f, 0);
		instance.transform.localScale = new Vector3(spawnScale * map.transform.localScale.x, spawnScale * map.transform.localScale.y, spawnScale * map.transform.localScale.z);
		instance.transform.SetParent(map.transform);
		instance.GetComponent<Area>().map = map;
		instance.GetComponent<Area>().location = location;
		instance.GetComponent<Area>().areaType = areaType;
		instance.GetComponent<Area>().drugsCount = drugsCount;
    }
}
