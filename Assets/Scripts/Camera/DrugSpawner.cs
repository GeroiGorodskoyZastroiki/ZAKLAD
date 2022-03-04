using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugSpawner : MonoBehaviour
{
    void Start()
    {
        Object[] prefabs = Resources.LoadAll("Objects/3D/Drugs/Prefabs");
        Debug.Log(prefabs.Length);
        var randomPrefab = prefabs[Random.Range(0,prefabs.Length)];
        Instantiate(randomPrefab, gameObject.transform);
    }
}
