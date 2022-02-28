using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
	private void Update()
	{
		GetComponentInParent<Area>().transform.localPosition = _map.GeoToWorldPosition(location, true) + new Vector3(0, 0.1f, 0);
		//spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale); // относительное отображение
		GetComponentInParent<Area>().transform.localScale = new Vector3(_spawnScale * _map.transform.lossyScale.x, _spawnScale * _map.transform.lossyScale.y, _spawnScale * _map.transform.lossyScale.z);
	}
}
