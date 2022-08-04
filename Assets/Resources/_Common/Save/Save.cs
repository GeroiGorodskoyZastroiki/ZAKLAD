using System.Collections.Generic;

[System.Serializable]
public class Save
{
    public int drugsStats;
    public int drugsStock;

    public List<AreaData> areaData;
}

[System.Serializable]
public class AreaData
{
    public double[] location = new double[2];
    public string areaType;
    public int drugsCount;
}