﻿using System;
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

	[SerializeField]
	Splash splash;

	AreaManager areaManager;
	Area area;
	Player player;

	public TMP_Text timer;

    void Start()
	{
		NullTab();
		notification.gameObject.SetActive(false);
		Splash.onSplashEnd += ShowNotificationSave;
		player = playerObject.GetComponent<Player>();
		areaManager = map.GetComponentInChildren<AreaManager>();
	}

    private void Update()
    {
		UpdateUI();
	}

	public void NullTab()
    {
		SetCurrentTab(null);
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
				SceneManager.LoadSceneAsync("CameraNew", LoadSceneMode.Additive);
				break;
		}
	}

	public void ShowNotificationSave()
    {
		notification.gameObject.GetComponentInChildren<TMP_Text>().text = "В ДАННОЙ ВЕРСИИ ИГРЫ НЕТ СОХРАНЕНИЙ. ПРЕДПОЛАГАЕТСЯ ЦИКЛ: УСТАНОВИЛ → ПОРЖАЛ → УДАЛИЛ";
		notification.gameObject.SetActive(true);
	}

	public void ShowNotificationPursuit()
	{
		notification.gameObject.GetComponentInChildren<TMP_Text>().text = "ВЫ ПОПАЛИ В ПОЛИЦЕЙСКУЮ ЗАСАДУ. ВЫРВИТЕСЬ ИЗ ОЦЕПЛЕНИЯ, ПОКА НЕ ИСТЁК ТАЙМЕР";
		notification.gameObject.SetActive(true);
	}

	public void HideNotification()
    {
		notification.gameObject.SetActive(false);
    }

	public void SplashCompany()
	{
		StartCoroutine(splash.SplashCompany());
	}

	public void SplashEndGame()
	{
		StartCoroutine(splash.SplashEndGame());
	}

	void UpdateUI()
    {
		if (!areaManager.EscapeArea)
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
				if (!areaManager.PickUpArea && areaManager.DropAreas.Length == 0)
				{
					zoneButton.GetComponentInChildren<TMP_Text>().text = "Заказать товар";
				}
				else
				{
					zoneButton.GetComponentInChildren<TMP_Text>().text = "---";
				}
			}
		}
        else
        {
			zoneButton.GetComponentInChildren<TMP_Text>().text = "---";
		}

		drugsStockText.text = string.Format("В НАЛИЧИИ: {0} ШТ.", player.drugsStock);
		drugsStatsText.text = "Кладов: " + player.drugsStats.ToString();
    }
}
