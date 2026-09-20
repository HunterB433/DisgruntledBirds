using UnityEngine;

public class HandSummoner : MonoBehaviour
{
    [Tooltip("The Hand prefab to summon")]
    public GameObject handPrefab;

    /// <summary>
    /// Summons a hand with the specified attributes and location (using coordinates).
    /// </summary>
    /// <param name="spawnPosition">The position where the hand will be spawned</param>
    /// <param name="speed">Movement speed of the hand</param>
    /// <param name="distance">Movement distance of the hand</param>
    /// <param name="angle">Movement angle as a Vector3</param>
    /// <param name="spinX">Spin speed around the X-axis</param>
    /// <param name="spinY">Spin speed around the Y-axis</param>
    /// <param name="spinZ">Spin speed around the Z-axis</param>
    /// <param name="resizeSpeed">Speed of resizing</param>
    /// <param name="resizeScale">Scale variation for resizing</param>
    public void SummonHand(
        Vector3 spawnPosition,
        float speed,
        float distance,
        Vector3 angle,
        float spinX,
        float spinY,
        float spinZ,
        float resizeSpeed,
        float resizeScale)
    {
        if (handPrefab == null)
        {
            Debug.LogError("Hand Prefab is not assigned!");
            return;
        }

        // Instantiate the hand prefab at the specified position, with no rotation (Quaternion.identity)
        GameObject newHand = Instantiate(handPrefab, spawnPosition, Quaternion.identity);

        // Configure the hand's movement script
        HandMover handMovement = newHand.GetComponent<HandMover>();
        if (handMovement != null)
        {
            handMovement.speed = speed;
            handMovement.distance = distance;
            handMovement.angle = angle;
            handMovement.spinX = spinX;
            handMovement.spinY = spinY;
            handMovement.spinZ = spinZ;
            handMovement.resizeSpeed = resizeSpeed;
            handMovement.resizeScale = resizeScale;
        }
        else
        {
            Debug.LogError("Hand prefab is missing the HandMovement script!");
        }
    }

    /// <summary>
    /// Finds all hand prefabs in the scene and destroys them.
    /// </summary>
    public void DestroyAllHands()
    {
        // Find all HandMovement objects in the scene and destroy their parent objects
        HandMover[] hands = FindObjectsOfType<HandMover>();

        foreach (HandMover hand in hands)
        {
            Destroy(hand.gameObject); // Destroy the GameObject the HandMovement script is attached to
        }
    }
}
