using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int money;
    int xp;

    [SerializeField]
    Button zoneButton;

    void Update()
    {
        CheckPosition();
    }

    void CheckPosition()
    {
        Area[] areas = (Area[])FindObjectsOfType(typeof(Area));
        foreach (var area in areas)
        {
            float radius = area.GetComponent<SpriteRenderer>().sprite.textureRect.width * area.spawnScale;
            if (Vector2.Distance(new(gameObject.transform.position.x, gameObject.transform.position.z), new(area.transform.position.x, area.transform.position.z)) <= radius)
            {
                zoneButton.gameObject.SetActive(true);
                return;
            }
        }
        zoneButton.gameObject.SetActive(false);
    }
}