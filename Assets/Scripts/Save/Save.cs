using System.Collections.Generic;

[System.Serializable]
public class Save
{
    public int money;
    public int xp;
    public int level;
    public int drugs;

    public List<AreaData> areaData;

    //public time = System.DateTime.Now;
}

[System.Serializable]
public class AreaData
{
    public double[] location = new double[2];
    public string areaType;
    public int drugsCount;
}