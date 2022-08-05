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
        var NearestArea = PickObject.PickNearest<Area>(gameObject);
        if (NearestArea)
        {
            float distance = Vector3.Distance(NearestArea.transform.position, gameObject.transform.position);
            if (distance <= areaManager.GetRadius(NearestArea))
            {
                return NearestArea.GetComponent<Area>();
            }
        }
        return null;
    }
}