using UnityEngine;
using UnityEngine.UI;

public class RainbowPanel : MonoBehaviour
{
    public Image panelImage; // Assign this in the Unity editor
    public float speed = 0.01f; // Speed at which the color changes

    private float hue = 0;

    void Update()
    {
        // Increment hue based on speed and time, and ensure it loops back after 1
        hue += speed * Time.deltaTime;
        if (hue > 1) hue = 0;

        // Convert HSV to RGB, using full saturation and brightness
        Color newColor = Color.HSVToRGB(hue, 1, 1);

        // Apply the color to the panel's Image component
        panelImage.color = newColor;
    }
}
