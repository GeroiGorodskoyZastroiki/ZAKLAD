using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Android;
using TMPro;

public class Permissions : MonoBehaviour
{
    [SerializeField]
    GameObject notificationPrefab;

    string deniedNotification = "�� ��������� ����������, ������������ ���������� � ���������� � ������������� ����";
    string noGPSNotification = "�������� GPS ��� ������ ����� � ������������� ����";

    void Start()
    {
        PermissionCallbacks permissionCallback = new PermissionCallbacks();
        permissionCallback.PermissionGranted += PermissionGranted;
        permissionCallback.PermissionDenied += PermissionDenied;
        permissionCallback.PermissionDeniedAndDontAskAgain += PermissionDenied;
        string[] permissions = {Permission.FineLocation, Permission.Camera};
        Permission.RequestUserPermissions(permissions, permissionCallback);
    }

    void PermissionGranted(string permission)
    {
        CheckGPS();
    }

    void PermissionDenied(string permission)
    {
        var notification = Instantiate(notificationPrefab, gameObject.transform);
        notification.GetComponentInChildren<TMP_Text>().text = deniedNotification;
    }

    void CheckGPS()
    {
        if (new LocationService().isEnabledByUser)
        {
            SceneManager.LoadScene("Map");
        }
        else
        {
            var notification = Instantiate(notificationPrefab, gameObject.transform);
            notification.GetComponentInChildren<TMP_Text>().text = noGPSNotification;
        }
    }
}
