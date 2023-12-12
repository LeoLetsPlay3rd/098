using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText;

    // Original color of the text
    private Color originalColor;

    // TMP gradient colors for hover effect
    public Color hoverGradientStart = new Color(1f, 0.5f, 0f);
    public Color hoverGradientMiddle = Color.white;
    public Color hoverGradientEnd = Color.white;

    // Smoothness factor for the color interpolation
    public float colorTransitionSpeed = 5f;

    // Target colors for interpolation
    private Color targetStartColor;
    private Color targetMiddleColor;
    private Color targetEndColor;

    // Whether the interpolation is in progress
    private bool isInterpolating = false;

    // Start is called before the first frame update
    void Start()
    {
        // Store the original color of the text
        originalColor = buttonText.color;

        // Set initial target colors
        targetStartColor = originalColor;
        targetMiddleColor = originalColor;
        targetEndColor = originalColor;
    }

    // Function to handle hover enter
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set target colors for hover
        targetStartColor = hoverGradientStart;
        targetMiddleColor = hoverGradientMiddle;
        targetEndColor = hoverGradientEnd;

        // Start color interpolation
        isInterpolating = true;
    }

    // Function to handle hover exit
    public void OnPointerExit(PointerEventData eventData)
    {
        // Set target colors to original color
        targetStartColor = originalColor;
        targetMiddleColor = originalColor;
        targetEndColor = originalColor;

        // Start color interpolation
        isInterpolating = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Interpolate between colors for a smoother transition
        if (isInterpolating)
        {
            Color interpolatedStart = Color.Lerp(buttonText.colorGradient.topLeft, targetStartColor, Time.deltaTime * colorTransitionSpeed);
            Color interpolatedMiddle = Color.Lerp(buttonText.colorGradient.topRight, targetMiddleColor, Time.deltaTime * colorTransitionSpeed);
            Color interpolatedEnd = Color.Lerp(buttonText.colorGradient.bottomRight, targetEndColor, Time.deltaTime * colorTransitionSpeed);

            // Apply TMP gradient color effect for hover
            buttonText.colorGradient = new VertexGradient(interpolatedStart, interpolatedMiddle, interpolatedMiddle, interpolatedEnd);

            // Check if interpolation is complete
            if (buttonText.colorGradient.topLeft == targetStartColor &&
                buttonText.colorGradient.topRight == targetMiddleColor &&
                buttonText.colorGradient.bottomRight == targetEndColor)
            {
                isInterpolating = false;
            }
        }
    }
}
