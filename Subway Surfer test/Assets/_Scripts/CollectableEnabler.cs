using UnityEngine;

/// <summary>
/// This enables collectables as they enter the collider, and disables them as they leave.
/// The purpose is to reduce processing work from collectables that are far away
/// </summary>
public class CollectableEnabler : MonoBehaviour
{
    public LayerMask collectableLayer;

    private Collider triggerZone;
    private IDeactivatable deactivatable;

    // Start is called before the first frame update
    void Start () {
        triggerZone = GetComponent<Collider> ();
    }

    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Collectable")) {
            deactivatable = other.gameObject.GetComponent<IDeactivatable> ();
            if (deactivatable != null)
                deactivatable.Activate ();
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.CompareTag ("Collectable")) {
            deactivatable = other.gameObject.GetComponent<IDeactivatable> ();
            if (deactivatable != null)
                deactivatable.Deactivate ();
        }
    }
}
