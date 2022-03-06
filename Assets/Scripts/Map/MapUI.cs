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

	[SerializeField]
	Button dropAllButton;

	[SerializeField]
	Text lvlText;

	[SerializeField]
	Text xpText;

	[SerializeField]
	Text moneyText;

	AreaSpawner areaSpawner;
	Area area;
	Player player;

	void Start()
    {
		player = playerObject.GetComponent<Player>();
		areaSpawner = map.GetComponentInChildren<AreaSpawner>();
		orderButton.onClick.AddListener(() => areaSpawner.SpawnArea("PickUp", 1, int.Parse(orderInputField.text)));
		zoneButton.onClick.AddListener(() => UseZone());
		dropAllButton.onClick.AddListener(() => DropAll());
	}

	private void DropAll()
    {
		player.drugs = 0;
		var areas = map.GetComponentsInChildren<Area>();
		foreach (var area in areas)
        {
			area.gameObject.Destroy();
        }
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
					player.drugs = area.drugsCount;
				}
				else
				{
					area.gameObject.Destroy();
					player.drugs--;
					SaveManager.Instance.PrepareSave();
					SaveManager.Instance.Save();
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
		lvlText.GetComponent<Text>().text = player.level.ToString();
		xpText.GetComponent<Text>().text = player.xp.ToString();
		moneyText.GetComponent<Text>().text = player.money.ToString();

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
