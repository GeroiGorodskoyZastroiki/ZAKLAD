using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] drugs;
    void Start()
    {
        var randomPrefab = drugs[Random.Range(0,drugs.Length)];
        Instantiate(randomPrefab, gameObject.transform);
    }
}
