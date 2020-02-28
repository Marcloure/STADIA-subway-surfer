using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Handles the Game Over state
/// </summary>
public class GameEnder : MonoBehaviour
{
    public Text gameOverText;

    private bool gameOver = false;
    private Collider endZone;
    private GameObject player;
    private PlayerController playerController;

    private void Start () {
        endZone = GetComponent<Collider> ();
        player = GameObject.FindGameObjectWithTag ("Player");
        playerController = player.GetComponent<PlayerController> ();
    }

    private void Update () {
        if (gameOver && Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) {
            Application.Quit ();
        }
    }

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject == player) {
            // Show Game Over text
            gameOverText.text = "Game Over!\nTap to exit game";
            gameOverText.gameObject.SetActive (true);

            // Stop the player avatar
            playerController.forcedStop = true;

            gameOver = true;
        }
    }
}
