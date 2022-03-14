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
		orderButton.onClick.AddListener(() => areaSpawner.SpawnAreas(1, "PickUp", int.Parse(orderInputField.text)));
		zoneButton.onClick.AddListener(() => UseZone());
		dropAllButton.onClick.AddListener(() => DropAll());
	}

	private void DropAll()
    {
		player.money = player.drugs * 1000;
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
					areaSpawner.SpawnAreas(area.drugsCount, "Drop", 1);
					DestroyImmediate(area.gameObject);
					player.drugs = area.drugsCount;
					SaveManager.Instance.Save("All");
				}
				else
				{
					DestroyImmediate(area.gameObject);
					player.drugs--;
					SaveManager.Instance.Save("All");
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
		if (Input.GetKeyDown(KeyCode.Space))
		{
			SaveManager.Instance.Load();
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
					zoneButton.gameObject.GetComponentInChildren<Text>().text = "���������";
				}
				else
				{
					zoneButton.gameObject.GetComponentInChildren<Text>().text = "��������";
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
    }
}
