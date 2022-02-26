using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript2 : MonoBehaviour
{
    private WebCamTexture backCamera;

    RawImage background;
    AspectRatioFitter fit;

    private void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        backCamera = new WebCamTexture(devices[0].name, Screen.width, Screen.height);
        background = GetComponent<RawImage>();
        fit = GetComponent<AspectRatioFitter>();

        backCamera.Play();
        background.texture = backCamera;
    }

    private void Update()
    {
        float ratio = (float)backCamera.width / (float)backCamera.height;
        fit.aspectRatio = ratio;

        float scaleY = backCamera.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);
        int orient = -backCamera.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}
