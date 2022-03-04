using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;

public class Area : MonoBehaviour
{
	GameObject area;
	public AbstractMap map;

	private void Awake()
    {
		area = gameObject;
		//map = area.GetComponentInParent<AbstractMap>();
	}

	public Vector2d location;

	public string areaType;

	public int drugsCount;

	public float spawnScale = 100f;

    private void Update()
    {
        if (map.transform.localScale.x < 0.35)
        {
            area.transform.GetChild(0).gameObject.SetActive(false);
            area.transform.GetChild(1).gameObject.SetActive(false);
            area.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            area.transform.GetChild(0).gameObject.SetActive(true);
            area.transform.GetChild(1).gameObject.SetActive(true);
            area.transform.GetChild(2).gameObject.SetActive(false);
        }
        area.transform.GetChild(1).gameObject.transform.localScale += new Vector3 (spawnScale * 2, spawnScale * 2, spawnScale * 2); //не работает, т.к. привязано к map

        if (areaType == "PickUp")
        {
            area.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            area.transform.GetChild(0).gameObject.SetActive(false);
        }
    }


    //Area привязана к Map, поэтому всё что происходит ниже - происходит автоматически

    //private void Update()
    //{
    //    if (area == null) { return; }

    //    //area.transform.position = map.GeoToWorldPosition(location, true);
    //    //area.transform.localPosition += new Vector3(0, 1, 0);
    //    //Debug.Log("AreaScale" + area.transform.lossyScale.x + "  AreaScaleLocal" + area.transform.localScale.x);
    //    //area.transform.localScale = new Vector3(spawnScale * map.transform.localScale.x, spawnScale * map.transform.localScale.y, spawnScale * map.transform.localScale.z);
    //}

    //spawnedObject.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale); // относительное отображение
}
