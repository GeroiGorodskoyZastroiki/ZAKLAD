using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTutorial : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
