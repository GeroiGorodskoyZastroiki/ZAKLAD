using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; set; }
    public Save save;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Load();
    }

    public void PrepareSave()
    {
        save.xp = Player.Instance.xp;
        save.level = Player.Instance.level;
        save.money = Player.Instance.money;
        save.drugs = Player.Instance.drugs;

        save.areaData.Clear();
        Area[] areas = (Area[])FindObjectsOfType(typeof(Area));
        foreach (Area area in areas)
        {
            AreaData areaData = new AreaData();
            areaData.location[0] = area.location.x;
            areaData.location[1] = area.location.y;
            areaData.areaType = area.areaType;
            areaData.drugsCount = area.drugsCount;
            save.areaData.Add(areaData);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString("save", Serializer.Serialize<Save>(save));
    }

    public void Load()
    {
        if ( PlayerPrefs.HasKey("save"))
        {
            save = Serializer.Deserialize<Save>(PlayerPrefs.GetString("save"));
        }
        else
        {
            save = new Save();
            Save();
        }
    }
}
