using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player movement and movement animation
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 7f;
    public float lateralSpeed = 10f;
    public bool forcedStop = false;

    [SerializeField]
    private Transform[] lanes = new Transform[3];
    private int currentLane = 1;
    private float changeLaneTime;
    private bool isChangingLane;
    private PlayerCollisionDetector collisionDetector;

    // Touch control variables
    private Vector2 startTouchPosition, endTouchPosition;

    // Animator
    private Animator anim;

    void Start () {
        anim = GetComponent<Animator> ();
        collisionDetector = GetComponent<PlayerCollisionDetector> ();

        // This considers that the lanes are equally far apart
        changeLaneTime = (lanes[1].position.x - lanes[0].position.x) / lateralSpeed;
    }

    void Update () {
        if (!collisionDetector.CollidesFront () && !forcedStop) {
            MoveForward ();
        }
        else {
            StopMoving ();
        }

#if UNITY_ANDROID
        MoveOnTouchDrag ();
#endif

#if UNITY_EDITOR
        MoveWithKeyboard ();
#endif
    }

    /// <summary>
    /// Moves the transform forward by forwardSpeed
    /// </summary>
    private void MoveForward () {
        transform.Translate (0f, 0f, forwardSpeed * Time.deltaTime);
        anim.SetFloat ("Speed", forwardSpeed);
    }

    /// <summary>
    /// Stops the player from moving and stops animation
    /// </summary>
    private void StopMoving () {
        anim.SetFloat ("Speed", 0f);
    }

    /// <summary>
    /// Compares touch starting position with ending position to change lane
    /// </summary>
    private void MoveOnTouchDrag () {
        if (Input.touchCount > 0) {
            if (Input.GetTouch (0).phase == TouchPhase.Began)
                startTouchPosition = Input.GetTouch (0).position;

            else if (Input.GetTouch (0).phase == TouchPhase.Ended) {
                endTouchPosition = Input.GetTouch (0).position;

                // Move left or right depending on touch movement
                if (endTouchPosition.x < startTouchPosition.x && currentLane > 0)
                    StartCoroutine (ChangeLane (currentLane - 1));
                else if (endTouchPosition.x > startTouchPosition.x && currentLane < lanes.Length - 1)
                    StartCoroutine (ChangeLane (currentLane + 1));
            }
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Change the lane with keyboard's A or D key
    /// </summary>
    private void MoveWithKeyboard () {
        // Move to the left
        if (Input.GetKeyDown (KeyCode.A) && currentLane > 0)
            StartCoroutine (ChangeLane (currentLane - 1));

        // Move to the right
        else if (Input.GetKeyDown (KeyCode.D) && currentLane < lanes.Length - 1 && !collisionDetector.CollidesRight ()) {
            StartCoroutine (ChangeLane (currentLane + 1));
        }
    }
#endif

/// <summary>
/// Lerps the transform from its lane to a new one in changeLaneTime
/// </summary>
private IEnumerator ChangeLane (int newLane) {
        while (isChangingLane)
            yield return null;

        // Break if newLane is not free
        if (currentLane > newLane && collisionDetector.CollidesLeft ())
            yield break;
        else if (currentLane < newLane && collisionDetector.CollidesRight ())
            yield break;

        // elapsedTime is the time passed in the animation
        float elapsedTime = 0;
        float timeRatio;
        Vector3 startingPos = transform.position;
        Vector3 newPos;

        currentLane = newLane;
        isChangingLane = true;

        // Lerp and translate at each frame
        while (elapsedTime < changeLaneTime) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > changeLaneTime)
                elapsedTime = changeLaneTime;

            timeRatio = elapsedTime / changeLaneTime;

            newPos = Vector3.Lerp (startingPos, lanes[newLane].position, timeRatio);
            float translation = newPos.x - transform.position.x;
            transform.Translate (translation, 0f, 0f);

            // Set animation values. The quadratic function makes the direction go from 0 to 1 to 0 again
            anim.SetFloat ("Speed", forwardSpeed);
            anim.SetFloat ("Direction", Mathf.Sign (translation) * (4 * (timeRatio) - 4 * (timeRatio * timeRatio)));

            yield return null;
        }

        // Done
        isChangingLane = false;

        // Reset animation direction
        anim.SetFloat ("Direction", 0f);
    }        
}