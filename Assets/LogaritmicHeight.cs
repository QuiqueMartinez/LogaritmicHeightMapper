using UnityEngine;
using UnityEngine.UI;

// Maps a normalized height to a lograitmic scaled height
// with more control in the lower heights.
// Not very optimized. Just for study purposes.

public class LogaritmicHeight : MonoBehaviour
{
    public Slider slider;
    public AnimationCurve curve;

    // Normalized height
    [Range(0, 1)]
    float height = 0;

    // Mouse wheel sensitivity
    public float sensitivity = 1;

    // Mapped max height
    public float maxHeight = 500;

    // Mappedmin height
    public float minHeight = 1;
    
    // The higher, the more control (1 Linear)
    public uint exponent = 3;

    void Start()
    {
        // Sync height with slider
        slider.onValueChanged.AddListener ((value) => height = value);
    }

    void Update()
    {
        // Mapping
        // scaledHeight = A * C^height + B; 
        // C = 10 ^ baseExponent;

        float C = (uint) Mathf.Pow(10, exponent);
        float A = (maxHeight - minHeight) / (C + 1);
        float B = minHeight - A;
        float scaledHeight = (A * Mathf.Pow(C, curve.Evaluate(height))) + B;

        transform.position =  new Vector3(0, scaledHeight, -10);
    }

    private void OnGUI()
    {
        // Applies mouse wheel
        height += Input.mouseScrollDelta.y * sensitivity;
        
        // Sync Slider
        slider.value = height;
    }
}
