using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

public class Permissions : MonoBehaviour
{
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
        
    }

    void CheckGPS()
    {
        if (new LocationService().isEnabledByUser)
        {
            SceneManager.LoadScene("Map");
        }
        else
        {

        }
    }
}
