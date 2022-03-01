using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugSpawner : MonoBehaviour
{
    var prefabs : Object[] = Resources.LoadAll("");
    void Start()
    {
        var randomPrefab = prefabs[Random.Range(prefabs.Count)];
        Instantiate(randomPrefab);
    }
}
