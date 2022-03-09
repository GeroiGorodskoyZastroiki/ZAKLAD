using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Splash : MonoBehaviour
{
    [SerializeField]
    AudioSource burp;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (SplashScreen.isFinished)
        {
            if (!gameObject.activeSelf)
            {
                Invoke("ActivateSplash", 4);
            }
        }
    }

    private void ActivateSplash()
    {
        burp.Play();
        gameObject.SetActive(true);
    }
}
