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
	GameObject notificationPrefab;

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

	public TMP_Text timer;

	AreaManager areaManager;
	Area area;
	Player player;

	public string saveNotification = "СЕЙЧАС В ИГРЕ НЕТ СОХРАНЕНИЙ. ПРЕДПОЛАГАЕТСЯ ЦИКЛ: УСТАНОВИЛ - ПОРЖАЛ - УДАЛИЛ";
	public string pursuitNotification = "ВЫ ПОПАЛИ В ПОЛИЦЕЙСКУЮ ЗАСАДУ. ВЫРВИТЕСЬ ИЗ ОЦЕПЛЕНИЯ, ПОКА НЕ ИСТЁК ТАЙМЕР";
	public string tutorialNotification = "ЗАКАЗЫВАЙТЕ ТОВАР, ПОДБИРАЙТЕ ЕГО, РАЗНОСИТЕ ПО АДРЕСАМ И СБРАСЫВАЙТЕ";

	void Start()
	{
		NullTab();
		Splash.onSplashEnd += SplashEnded;
		player = playerObject.GetComponent<Player>();
		areaManager = map.gameObject.GetComponent<AreaManager>();
	}

    void Update()
    {
		UpdateUI();
	}

    #region [Tabs]

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

    #endregion [Tabs]

    #region [Settings]

    public void RegenerateZones()
    {
		areaManager.RegenerateZones();
	}

    #endregion [Settings]

    #region [Work]

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

	#endregion [Work]

	#region [Notifications]

	public void ShowNotification(string text)
    {
		Instantiate(notificationPrefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform).gameObject.GetComponentInChildren<TMP_Text>().text = text;
	}

	public void ForceHideNotification()
    {
		var notification = FindObjectOfType<Notification>();
		if (notification)
		{
			Destroy(notification.gameObject);
		}
    }

    #endregion [Notifications]

    #region [Splash]

    public void SplashCompany()
	{
		StartCoroutine(splash.SplashCompany());
	}

	public void SplashEndGame()
	{
		StartCoroutine(splash.SplashEndGame());
	}

	void SplashEnded()
	{
		ShowNotification(tutorialNotification);
		ShowNotification(saveNotification);
	}

    #endregion [Splash]

    void UpdateUI()
    {
		var zoneButtonText = zoneButton.GetComponentInChildren<TMP_Text>();
		if (!areaManager.EscapeArea)
        {
			if (player.inArea)
			{
				if (player.inArea.GetType().Name == "PickUpArea")
				{
					zoneButtonText.text = "Подобрать товар";
				}
				else
				{
					zoneButtonText.text = "Сбросить товар";
				}
			}
			else
			{
				if (!areaManager.PickUpArea && areaManager.DropAreas.Length == 0)
				{
					zoneButtonText.text = "Заказать товар";
				}
				else
				{
					zoneButtonText.text = "---";
				}
			}
		}
        else
        {
			zoneButtonText.text = "---";
		}

		drugsStockText.text = string.Format("В НАЛИЧИИ: {0} ШТ.", player.drugsStock);
		drugsStatsText.text = "Кладов: " + player.drugsStats.ToString();
    }
}
