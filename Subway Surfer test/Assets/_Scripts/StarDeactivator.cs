using UnityEngine;

/// <summary>
/// Applies the IDeactivatable interface to make custom deactivation procedures
/// </summary>
public class StarDeactivator : MonoBehaviour, IDeactivatable
{
    private MeshRenderer meshRenderer;
    private AnimationScript animationScript;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer> ();
        animationScript = GetComponent<AnimationScript> ();
    }

    /// <summary>
    /// Activate the renderer and animation of the object
    /// </summary>
    public void Activate () {
        meshRenderer.enabled = true;
        animationScript.enabled = true;
    }

    /// <summary>
    /// Deactivates the renderer and animation of the object
    /// </summary>
    public void Deactivate () {
        meshRenderer.enabled = false;
        animationScript.enabled = false;
    }
}
