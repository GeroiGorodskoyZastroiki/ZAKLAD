using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;

public class Player : MonoBehaviour
{
    int money;
    int xp;
    int level;
    public int drugs;

    public Area? neareastArea;

    private void Start()
    {
        var map = gameObject.GetComponentInParent<AbstractMap>();
    }

    void Update()
    {
        neareastArea = CheckPosition();
    }

    Area CheckPosition()
    {
        Area[] areas = (Area[])FindObjectsOfType(typeof(Area));
        Area minDistanceArea = null;
        float minDistance = 10000;
        foreach (var area in areas)
        {
            float textureRadius = area.GetComponent<SpriteRenderer>().sprite.textureRect.width / 2;
            float density = area.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
            float radius = (textureRadius / density) * area.spawnScale;
            float distance = Vector2.Distance(new(gameObject.transform.position.x, gameObject.transform.position.z), new(area.transform.position.x, area.transform.position.z));
            if (distance <= radius & distance < minDistance)
            {
                //Debug.Log(radius);
                minDistanceArea = area;
                minDistance = distance;
            }
        }
        return minDistanceArea;
    }
}