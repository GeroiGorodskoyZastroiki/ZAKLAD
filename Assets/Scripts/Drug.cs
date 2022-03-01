using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drug : MonoBehaviour
{
    void Start()
    {
        SwipeDetection.SwipeEvent += OnSwipe;
    }

    private void OnSwipe(Vector2 direction)
    {

    }

    void Update()
    {

    }

    private void Move(Vector3 direction)
    {
        
    }
}
