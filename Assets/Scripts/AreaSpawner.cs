using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

	[Geocode]
	Vector2d[] _locations;

	[SerializeField]
	float _spawnScale = 100f;

	[SerializeField]
	GameObject _markerPrefab;

	List<GameObject> _spawnedObjects;

	public void SpawnArea(int areaCount)
	{
		_locations = new Vector2d[areaCount];
		_spawnedObjects = new List<GameObject>();
		for (int i = 0; i < areaCount; i++)
		{
			_locations[i] = _map.WorldToGeoPosition(player.transform.position + new Vector3(Random.Range(-100,100), 0, Random.Range(-100, 100)));
			var instance = Instantiate(_markerPrefab);
			instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
			instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			_spawnedObjects.Add(instance);
		}
	}

	private void Update() //перенести в спавнящиеся префабы
	{
		int count = _spawnedObjects.Count;
		for (int i = 0; i < count; i++)
		{
			var spawnedObject = _spawnedObjects[i];
			var location = _locations[i];
			spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true) + new Vector3(0, 0.1f, 0);
			//spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale); // относительное отображение
			spawnedObject.transform.localScale = new Vector3(_spawnScale * _map.transform.lossyScale.x, _spawnScale * _map.transform.lossyScale.y, _spawnScale * _map.transform.lossyScale.z);
		}
	}
}
