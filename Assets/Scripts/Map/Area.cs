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

    private void Update()
    {
        if (map.transform.localScale.x < 0.35)
        {
            area.transform.GetChild(0).gameObject.SetActive(false);
            area.transform.GetChild(1).gameObject.SetActive(false);
            //area.transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            area.transform.GetChild(0).gameObject.SetActive(true);
            area.transform.GetChild(1).gameObject.SetActive(true);
            //area.transform.GetChild(2).gameObject.SetActive(false);
        }

        if (areaType == "PickUp")
        {
            area.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            area.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    //spawnedObject.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale); // относительное отображение
}
