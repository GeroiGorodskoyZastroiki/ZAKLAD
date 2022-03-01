using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugSpawner : MonoBehaviour
{
    Object[] prefabs = Resources.LoadAll("/ZAKLAD/Assets/Prefabs/Objects/Models/Drugs");
    void Start()
    {
        var randomPrefab = prefabs[Random.Range(0,prefabs.Length)];
        Instantiate(randomPrefab);
    }
}
