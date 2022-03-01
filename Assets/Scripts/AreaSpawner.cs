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
	float spawnScale = 100f;

	[SerializeField]
	Button orderButton;

	[SerializeField]
	InputField orderInputField;

	[SerializeField]
	Button zoneButton;

	AbstractMap map;
	Vector2d[] locations;
	int drugs;

	public void Start()
	{
		map = gameObject.GetComponent<AbstractMap>();
		orderButton.onClick.AddListener(() => SpawnArea("PickUp", 1, drugs = int.Parse(orderInputField.text)));
		zoneButton.onClick.AddListener(() => SpawnArea("Drop", drugs, 1));
	}

    public void SpawnArea(string areaType, int areaCount, int drugsCount)
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
			instance.GetComponent<Area>().drugsCount = drugsCount;
		}
	}

    public void Update()
    {
        if (drugs>0)
        {
			orderButton.enabled = false;
        }
        else
        {
			orderButton.enabled = true;
        }
    }
}
