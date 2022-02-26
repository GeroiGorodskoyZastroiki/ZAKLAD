namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.Location;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;

	public class DrugSpawner : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		GameObject player;

		private Transform playerTransform;

		[Geocode]
		//private Vector3d[] _locations;

		[SerializeField]
		float _spawnScale = 100f;

		[SerializeField]
		GameObject _markerPrefab;

		List<GameObject> _spawnedObjects;

		public void CreateDrugs(int drugsCount)
		{
			playerTransform = player.GetComponent<Transform>();
			_spawnedObjects = new List<GameObject>();
			for (int i = 0; i < drugsCount; i++)
			{
				var instance = Instantiate(_markerPrefab);
				instance.transform.localPosition = new Vector3(player.transform.position.x + Random.Range(-100,100), 0.05f, player.transform.position.z + Random.Range(-100, 100));
				instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				_spawnedObjects.Add(instance);
			}
		}

		private void Update()
		{
			int count = _spawnedObjects.Count;
			for (int i = 0; i < count; i++)
			{
				var spawnedObject = _spawnedObjects[i];
				//var location = _locations[i];
				//spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
				spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			}
		}
	}
}