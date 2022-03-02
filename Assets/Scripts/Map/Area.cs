using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;

public class Area : MonoBehaviour
{
	GameObject area;
	AbstractMap map;

	private void Awake()
    {
		area = gameObject;
		map = area.GetComponentInParent<AbstractMap>();
	}

	public Vector2d location;

	public string areaType;

	public int drugsCount;

	public float spawnScale = 100f;

	public void UseZone()
    {
		if (areaType == "PickUp")
        {
			map.GetComponent<AreaSpawner>().SpawnArea("Drop", drugsCount, 1);
        }
        else
        {
			SceneManager.LoadScene("CameraNew");
        }
    }

	private void Update()
	{
		if (area == null) { return; }

		area.transform.position = map.GeoToWorldPosition(location, true);
		area.transform.localPosition += new Vector3(1, 1, 1);
		//spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale); // относительное отображение
		area.transform.localScale = new Vector3(spawnScale * map.transform.lossyScale.x, spawnScale * map.transform.lossyScale.y, spawnScale * map.transform.lossyScale.z);
	}
}
