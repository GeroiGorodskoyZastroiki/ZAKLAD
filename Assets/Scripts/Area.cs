using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;

public class Area : MonoBehaviour
{
	public Area(AbstractMap _map, Vector2d _location, float _spawnScale)
    {
		map = _map;
		location = _location;
		spawnScale = _spawnScale;
    }

	[Geocode]
	Vector2d location;

	AbstractMap map;

	float spawnScale = 100f;

	private void Update()
	{
		GetComponentInParent<Area>().transform.localPosition = map.GeoToWorldPosition(location, true) + new Vector3(0, 0.1f, 0);
		//spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale); // относительное отображение
		GetComponentInParent<Area>().transform.localScale = new Vector3(spawnScale * map.transform.lossyScale.x, spawnScale * map.transform.lossyScale.y, spawnScale * map.transform.lossyScale.z);
	}
}
