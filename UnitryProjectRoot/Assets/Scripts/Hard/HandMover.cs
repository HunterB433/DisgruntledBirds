using UnityEngine;

public class HandMover: MonoBehaviour
{
    [Tooltip("Speed of the up and down movement")]
    public float speed = 2f;

    [Tooltip("How far the hand moves up and down")]
    public float distance = 2f;

    [Tooltip("Directional angle for movement (normalized vector preferred)")]
    public Vector3 angle = new Vector3(1, 0, 0);

    [Tooltip("Spin speed around the X-axis in degrees per second")]
    public float spinX = 0f;

    [Tooltip("Spin speed around the Y-axis in degrees per second")]
    public float spinY = 50f;

    [Tooltip("Spin speed around the Z-axis in degrees per second")]
    public float spinZ = 0f;

    [Tooltip("Speed of resizing")]
    public float resizeSpeed = 1f;

    [Tooltip("Maximum scale change for resizing")]
    public float resizeScale = 0.5f;

    private Vector3 startPosition;
    private Vector3 originalScale;

    void Start()
    {
        startPosition = transform.position;
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Move the hand at an angle using a sine wave
        Vector3 offset = angle.normalized * Mathf.Sin(Time.time * speed) * distance;
        transform.position = startPosition + offset;

        // Spin the hand on each axis
        transform.Rotate(new Vector3(spinX, spinY, spinZ) * Time.deltaTime, Space.Self);

        // Resize the hand using a sine wave
        float scaleFactor = 1 + Mathf.Sin(Time.time * resizeSpeed) * resizeScale;
        transform.localScale = originalScale * scaleFactor;
    }
}
