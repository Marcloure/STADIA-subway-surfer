using UnityEngine;

/// <summary>
/// Detects with there is an object at the front or sides of the player
/// </summary>
public class PlayerCollisionDetector : MonoBehaviour
{
    public float frontRayRange, sideRayRange;
    public float sideRaysAngle;
    public Transform rayOrigin;
    public LayerMask rayLayer;

#if UNITY_EDITOR
    private void Update () {
        Debug.DrawRay (rayOrigin.position, Quaternion.AngleAxis (sideRaysAngle, Vector3.up) * transform.forward * sideRayRange, Color.green);
        Debug.DrawRay (rayOrigin.position, transform.forward * frontRayRange, Color.blue);
        Debug.DrawRay (rayOrigin.position, Quaternion.AngleAxis (-sideRaysAngle, Vector3.up) * transform.forward * sideRayRange, Color.red);
    }
#endif

    /// <summary>
    /// Detects frontal collision
    /// </summary>
    /// <returns>True if the player collides at front</returns>
    public bool CollidesFront () {
        return Physics.Raycast (rayOrigin.position, transform.forward, frontRayRange, rayLayer.value);
    }

    /// <summary>
    /// Detects if there is an object at the player's left
    /// </summary>
    /// <returns>True if the player collides at the left</returns>
    public bool CollidesLeft () {
        return Physics.Raycast (rayOrigin.position, Quaternion.AngleAxis (-sideRaysAngle, Vector3.up) * transform.forward, sideRayRange, rayLayer.value);
    }

    /// <summary>
    /// Detects if there is an object at the player's right
    /// </summary>
    /// <returns>True if the player collides at the right</returns>
    public bool CollidesRight () {
        return Physics.Raycast (rayOrigin.position, Quaternion.AngleAxis (sideRaysAngle, Vector3.up) * transform.forward, sideRayRange, rayLayer.value);
    }
}
