using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class MapUI : MonoBehaviour
{
	[SerializeField]
	AbstractMap map;

	[SerializeField]
	GameObject playerObject;

	[SerializeField]
	Button orderButton;

	[SerializeField]
	InputField orderInputField;

	[SerializeField]
	Button zoneButton;

	AreaSpawner areaSpawner;
	Area area;
	Player player;

	void Start()
    {
		player = playerObject.GetComponent<Player>();
		areaSpawner = map.GetComponentInChildren<AreaSpawner>();
		orderButton.onClick.AddListener(() => areaSpawner.SpawnArea("PickUp", 1, int.Parse(orderInputField.text)));
		zoneButton.onClick.AddListener(() => UseZone());
	}

	private void UseZone()
    {
		try
		{
			area = player.neareastArea;
			if (area)
			{
				if (area.areaType == "PickUp")
				{
					areaSpawner.SpawnArea("Drop", area.drugsCount, 1);
					area.gameObject.Destroy();
					Debug.Log("PickUp");
					player.drugs = area.drugsCount;
				}
				else
				{
					area.gameObject.Destroy();
					player.drugs--;
					Debug.Log("Drop");
					SceneManager.LoadScene("CameraNew");
				}
			}
            else
            {
				throw new NullReferenceException();
			}
		}
		catch (NullReferenceException) { }
	}

	void Update()
	{
        try
        {
			area = player.neareastArea;
			if (area)
			{
				zoneButton.gameObject.SetActive(true);
				if (area.areaType == "PickUp")
				{
					zoneButton.gameObject.GetComponentInChildren<Text>().text = "Подобрать";
				}
				else
				{
					zoneButton.gameObject.GetComponentInChildren<Text>().text = "Оставить";
				}
			}
            else
            {
				throw new NullReferenceException();
            }
		}
		catch (NullReferenceException)
        {
			zoneButton.gameObject.SetActive(false);
		}

		Area[] areas = (Area[])FindObjectsOfType(typeof(Area));
		if (areas.Length > 0)
        {
            orderButton.gameObject.SetActive(false);
            orderInputField.gameObject.SetActive(false);
        }
        else
        {
            orderButton.gameObject.SetActive(true);
            orderInputField.gameObject.SetActive(true);
        }
    }
}
