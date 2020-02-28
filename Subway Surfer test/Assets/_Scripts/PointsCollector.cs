using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Picks up collectables and registers the score
/// </summary>
public class PointsCollector : MonoBehaviour
{
    public float score = 0;
    public Text scoreTextUI;
    
    public void PickUp (GameObject gameObject) {
        score++;
        scoreTextUI.text = "Score: " + score;
        gameObject.SetActive (false);
    }
}
