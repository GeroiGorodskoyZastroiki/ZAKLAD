using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    private bool camAvaliable;
    private WebCamTexture backCamera;
    private Texture defaultTexture;

    public RawImage background;
    public AspectRatioFitter fit;

    private void Start()
    {
        defaultTexture = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No camera avaliable");
            camAvaliable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                backCamera = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
            }
        }

        if (backCamera == null)
        {
            return;
        }

        backCamera.Play();
        background.texture = backCamera;
        camAvaliable = true;
    }

    private void Update()
    {
        if (!camAvaliable)
        {
            return;
        }

        float ratio = (float)backCamera.width / (float)backCamera.height;
        fit.aspectRatio = ratio;

        float scaleY = backCamera.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);
        int orient = -backCamera.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
    }
}
