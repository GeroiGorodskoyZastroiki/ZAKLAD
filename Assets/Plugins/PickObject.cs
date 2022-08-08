using UnityEngine;
using System.Linq;

public static class PickObject
{
    public static GameObject PickNearest(GameObject MyObject, string Tag)
    {
        GameObject[] AllObjects = GameObject.FindGameObjectsWithTag(Tag);
        return PickNearest(MyObject, AllObjects);
    }

    public static GameObject PickNearest<T>(GameObject MyObject) where T : MonoBehaviour
    {
        GameObject[] AllObjects = ((MonoBehaviour[])Object.FindObjectsOfType(typeof(T))).Select(x => x.gameObject).ToArray();
        return PickNearest(MyObject, AllObjects);
    }

    static GameObject PickNearest(GameObject MyObject, GameObject[] AllObjects)
    {
        if (AllObjects.Length == 0) return null;
        return AllObjects.OrderBy(x => Vector3.Distance(x.transform.position, MyObject.transform.position))
            .Where(x => CheckRelation(MyObject, x) == false)
            .First(x => Vector3.Distance(x.transform.position, MyObject.transform.position) >= 0);
    }

    static bool CheckRelation(GameObject MyObject, GameObject OtherObject)
    {
        if (MyObject == OtherObject) return true;
        if (OtherObject.transform.IsChildOf(MyObject.transform)) return true;
        return false;
    }
}
