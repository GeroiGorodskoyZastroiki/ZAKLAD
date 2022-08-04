using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    void Start()
    {
        Destroy((transform.Find("Background")).gameObject, 3.5f);
        Destroy((transform.Find("Image")).gameObject, 4.8f);
    }
}
