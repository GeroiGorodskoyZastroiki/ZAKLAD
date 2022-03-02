using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;

public class Player : MonoBehaviour
{
    int money;
    int xp;
    int level;

    [SerializeField]
    Button zoneButton;

    void Update()
    {
        CheckPosition();
    }

    private void Start()
    {
        var map = gameObject.GetComponentInParent<AbstractMap>();
    }

    void CheckPosition()
    {
        Area[] areas = (Area[])FindObjectsOfType(typeof(Area));
        foreach (var area in areas)
        {
            float textureRadius = area.GetComponent<SpriteRenderer>().sprite.textureRect.width / 2;
            float density = area.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
            float radius = (textureRadius / density) * area.spawnScale;
            if (Vector2.Distance(new(gameObject.transform.position.x, gameObject.transform.position.z), new(area.transform.position.x, area.transform.position.z)) <= radius)
            {
                Debug.Log(radius);
                zoneButton.gameObject.SetActive(true);
                if (area.areaType == "PickUp")
                {
                    zoneButton.gameObject.GetComponentInChildren<Text>().text = "Подобрать";
                }
                else
                {
                    zoneButton.gameObject.GetComponentInChildren<Text>().text = "Оставить";
                }
                return;
            }
        }
        zoneButton.gameObject.SetActive(false);
    }
}