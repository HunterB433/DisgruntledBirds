using UnityEngine;

public class RandomColor : MonoBehaviour
{
    // List of vibrant colors (Rainbow shades)
    private readonly Color[] vibrantColors = {
        Color.red,
        new Color(1f, 0.5f, 0f), // Orange
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        new Color(0.5f, 0f, 1f)  // Violet
    };

    void Start()
    {
        // Get a random vibrant color from the array
        Color randomColor = vibrantColors[Random.Range(0, vibrantColors.Length)];

        // Apply the random color to both the body and the knot
        foreach (Transform child in transform)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = randomColor;
            }
        }
    }
}
