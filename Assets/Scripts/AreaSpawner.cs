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
	AbstractMap _map;

	[SerializeField]
	GameObject player;

	[SerializeField]
	InputField order;

	[SerializeField]
	float _spawnScale = 100f;

	[SerializeField]
	GameObject _markerPrefab;

	[Geocode]
	Vector2d[] _locations;

	public void SpawnArea()
	{
		var areaCount = (int)order.text;
		_locations = new Vector2d[areaCount];
		for (int i = 0; i < areaCount; i++)
		{
			_locations[i] = _map.WorldToGeoPosition(player.transform.position + new Vector3(Random.Range(-100,100), 0, Random.Range(-100, 100)));
			var instance = Instantiate(_markerPrefab);
			instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
			instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
		}
	}
}
