using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; set; }
    public Save save;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        XmlLoad();
    }

    public void Save()
    {
        save.xp = Player.Instance.xp;
        save.level = Player.Instance.level;
        save.money = Player.Instance.money;
        save.drugs = Player.Instance.drugs;

        save.areaData.Clear();
        Area[] areas = (Area[])FindObjectsOfType(typeof(Area));
        Debug.Log(areas.Length);
        foreach (Area area in areas)
        {
            AreaData areaData = new AreaData();
            areaData.location[0] = area.location.x;
            Debug.Log(area.location.x);
            areaData.location[1] = area.location.y;
            areaData.areaType = area.areaType;
            areaData.drugsCount = area.drugsCount;
            save.areaData.Add(areaData);
        }

        XmlSave();
    }

    public void XmlSave()
    {
        PlayerPrefs.SetString("save", Serializer.Serialize<Save>(save));
    }

    public void Load()
    {
        XmlLoad();

        Player.Instance.xp = save.xp;
        Player.Instance.level = save.level;
        Player.Instance.money = save.money;
        Player.Instance.drugs = save.drugs;

        var areaSpawner = (AreaSpawner)FindObjectOfType(typeof(AreaSpawner));
        if (save.areaData.Count > 0)
        {
            foreach (AreaData area in save.areaData)
            {
                Vector2d location = new Vector2d(area.location[0], area.location[1]);
                areaSpawner.LoadArea(location, area.areaType, area.drugsCount);
            }
        }
    }

    public void XmlLoad()
    {
        if ( PlayerPrefs.HasKey("save"))
        {
            save = Serializer.Deserialize<Save>(PlayerPrefs.GetString("save"));
        }
        else
        {
            save = new Save();
            XmlSave();
        }
    }
}
