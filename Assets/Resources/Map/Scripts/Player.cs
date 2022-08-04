using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }
    private void Awake()
    {
        Instance = this;
    }

    public int drugsStock = 0;
    public int drugsStats = 0;

    public Area inArea = null;

    public AbstractMap map;
    public AreaManager areaManager;

    void Update()
    {
        //map = GetComponentInParent<AbstractMap>();
        //areaManager = GetComponentInParent<AreaManager>();
        inArea = CheckInArea();
    }

    Area CheckInArea()
    {
        float GetRadius(GameObject area)
        {
            //float textureRadius = area.GetComponent<SpriteRenderer>().sprite.textureRect.width / 2;
            float textureRadius = 100;
            float density = area.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
            float radius = (textureRadius / density) * areaManager.spawnScale * map.transform.localScale.x;
            print(radius);
            return radius;
        }

        var NearestArea = PickObject.PickNearest<Area>(gameObject);
        if (NearestArea)
        {
            float distance = Vector3.Distance(NearestArea.transform.position, gameObject.transform.position);
            if (distance <= GetRadius(NearestArea))
            {
                return NearestArea.GetComponent<Area>();
            }
        }
        return null;
    }

    IEnumerator Pursuit()
    {
        var pursuitDistance = 0.0005;
        var initialPosition = map.WorldToGeoPosition(Instance.gameObject.transform.position);
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1);
            var currentPosition = map.WorldToGeoPosition(Instance.gameObject.transform.position);
            if (Vector2d.Distance(currentPosition, initialPosition) > pursuitDistance)
            {
                yield break;
            }
        }
        //концовка
    }
}