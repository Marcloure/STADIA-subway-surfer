using UnityEngine;

/// <summary>
/// Collides with collectables and sends them to the parent's collector
/// </summary>
public class CollectorSensor : MonoBehaviour
{
    private PointsCollector collector;

    // Start is called before the first frame update
    void Start()
    {
        collector = GetComponentInParent<PointsCollector> ();
    }

    // Update is called once per frame
    private void OnTriggerEnter (Collider other) {
        if (other.CompareTag ("Collectable")) {
            collector.PickUp (other.gameObject);
        }
    }
}
