using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class AdjustTextSize : MonoBehaviour
{
    public RectTransform backgroundRectTransform; // Assign this via the inspector

    private TextMeshProUGUI textMeshPro;
    private float backgroundWidth;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        backgroundWidth = backgroundRectTransform.sizeDelta.x;
    }

    public void UpdateScore(int score)
    {
        textMeshPro.text = $"Score: {score}";
        AdjustTextToFitBackground();
    }

    void AdjustTextToFitBackground()
    {
        // Calculate the width of the text
        Vector2 textSize = textMeshPro.GetPreferredValues();
        float textWidth = textSize.x;

        if (textWidth > backgroundWidth)
        {
            // If text width exceeds background width, scale down the font size
            while (textWidth > backgroundWidth && textMeshPro.fontSize > textMeshPro.fontSizeMin)
            {
                textMeshPro.fontSize--;
                textWidth = textMeshPro.GetPreferredValues().x;
            }
        }
        else
        {
            // If text width is less than background width, scale up the font size
            while (textWidth < backgroundWidth && textMeshPro.fontSize < textMeshPro.fontSizeMax)
            {
                textMeshPro.fontSize++;
                textWidth = textMeshPro.GetPreferredValues().x;

                // If text width exceeds background width, scale down the font size
                if (textWidth > backgroundWidth)
                {
                    textMeshPro.fontSize--;
                    break;
                }
            }
        }
    }
}
