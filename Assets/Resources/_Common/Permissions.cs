using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

public class Permissions : MonoBehaviour
{
    void Start()
    {
        PermissionCallbacks permissionCallback = new PermissionCallbacks();
        permissionCallback.PermissionGranted += PermissionAccepted;
        string[] permissions = {Permission.FineLocation, Permission.Camera};
        Permission.RequestUserPermissions(permissions, permissionCallback);
    }

    void PermissionAccepted(string permission)
    {
        SceneManager.LoadScene("Map");
    }
}
