using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using TMPro;

public class MapUI : MonoBehaviour
{
	[SerializeField]
	AbstractMap map;

	[SerializeField]
	GameObject playerObject;

	[SerializeField]
	RectTransform notification;

	[SerializeField]
	RectTransform background, settings, work, profile;

	[SerializeField]
	Button regenerateZones;

	[SerializeField]
	Button zoneButton;

	[SerializeField]
	TMP_InputField orderInputField;

	[SerializeField]
	TMP_Text drugsStockText;

	[SerializeField]
	TMP_Text drugsStatsText;

	AreaManager areaManager;
	Area area;
	Player player;

	void Start()
	{
		SetCurrentTab(null);
		notification.gameObject.SetActive(false);
		player = playerObject.GetComponent<Player>();
		areaManager = map.GetComponentInChildren<AreaManager>();
	}

    private void Update()
    {
		UpdateUI();
	}

    public void Settings()
	{
		SetCurrentTab(settings);
	}

	public void Work()
	{
		SetCurrentTab(work);
	}

	public void Profile()
	{
		SetCurrentTab(profile);
	}

	void SetCurrentTab(RectTransform currentTab)
    {
		if (currentTab == null)
        {
			background.gameObject.SetActive(false);
		}
        else
		{
			profile.gameObject.SetActive(false);
			work.gameObject.SetActive(false);
			settings.gameObject.SetActive(false);
			background.gameObject.SetActive(true);
			currentTab.gameObject.SetActive(true);
			background.sizeDelta = new Vector2(background.sizeDelta.x, 493f + currentTab.rect.height);
		}
	}

	public void RegenerateZones()
    {
		areaManager.RegenerateZones();
	}

	public void OrderMore()
    {
		orderInputField.text = (Math.Clamp((int.Parse(orderInputField.text) + 1), 1, 99)).ToString();
    }

	public void OrderLess()
	{
		orderInputField.text = (Math.Clamp((int.Parse(orderInputField.text) - 1), 1, 99)).ToString();
	}

	public void UseZone()
    {
		var zoneButtonText = zoneButton.GetComponentInChildren<TMP_Text>().text;
		switch (zoneButtonText)
        {
			case "Заказать товар":
				areaManager.SpawnPickUpArea(int.Parse(orderInputField.text));
				break;
			case "Подобрать товар":
				player.drugsStock = areaManager.PickUpArea.drugsCount;
				areaManager.SpawnDropAreas(int.Parse(orderInputField.text));
				break;
			case "Сбросить товар":
				LoadCamera();
				DestroyImmediate(player.inArea.gameObject); //Подождать корутиной?
				player.drugsStock--; //ДЕЛАТЬ ПОСЛЕ СБРОСА
				player.drugsStats++;
				break;
		}
	}

	void LoadCamera()
    {
		SceneManager.LoadSceneAsync("CameraNew", LoadSceneMode.Additive);
		map.gameObject.transform.root.gameObject.SetActive(false);
		gameObject.SetActive(false);
	}

	public void UnderstandNotification()
    {
		notification.gameObject.SetActive(false);
    }

	void UpdateUI()
    {
		if (player.inArea)
        {
			if (player.inArea.GetType().Name == "PickUpArea")
            {
				zoneButton.GetComponentInChildren<TMP_Text>().text = "Подобрать товар";
			}
            else
            {
				zoneButton.GetComponentInChildren<TMP_Text>().text = "Сбросить товар";
			}
		}
        else
        {
			if (!areaManager.PickUpArea)
            {
				zoneButton.GetComponentInChildren<TMP_Text>().text = "Заказать товар";
			}
            else
            {
				zoneButton.GetComponentInChildren<TMP_Text>().text = "---";
			}
		}

		drugsStockText.text = String.Format("В НАЛИЧИИ: {0} ШТ.", player.drugsStock);
		drugsStatsText.text = "Кладов: " + player.drugsStock.ToString();
    }
}
