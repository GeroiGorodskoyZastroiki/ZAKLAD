using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BezierFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector3 drugPosition;

    [SerializeField]
    private float speedModifier;

    private bool coroutineAllowed;

    private void Start()
    {
        SwipeDetection.SwipeEvent += Swiped;
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = true;
    }

    private void Swiped(Vector2 direction)
    {
        if (coroutineAllowed)
        {
            //if (Mathf.Abs(Input.acceleration.y) > 0.75)
            //{
                StartCoroutine(GoByTheRoute(routeToGo));
            //}
        }
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam<1)
        {
            tParam += Time.deltaTime * speedModifier;
            drugPosition=Mathf.Pow(1-tParam,3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = drugPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
            routeToGo = 0;

        var drugMesh = gameObject.transform.GetChild(1);
        drugMesh.gameObject.Destroy();
        //SaveManager.Instance.XmlSave();
        SceneManager.UnloadSceneAsync("CameraNew");
    }
}
