using UnityEngine;
using UnityEngine.UI;

public class LocateButton : MonoBehaviour
{
    Image image;

    [SerializeField]
    Sprite locate, locked, unlocked;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (CameraMovement.attachedToPlayer)
        {
            image.sprite = locked;
        }
        else
        {
            if (CameraMovement.Instance.CameraAbovePlayer())
            {
                image.sprite = unlocked;
            }
            else
            {
                image.sprite = locate;
            }
        }
    }

    public void OnClick()
    {
        if (CameraMovement.attachedToPlayer)
        {
            CameraMovement.attachedToPlayer = false;
        }
        else
        {
            if (CameraMovement.Instance.CameraAbovePlayer())
            {
                CameraMovement.attachedToPlayer = true;
            }
            else
            {
                CameraMovement.Instance.LocatePlayer();
            }
        }
    }
}
