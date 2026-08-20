using UnityEngine;
using UnityEngine.UI; 

public class HealthUI : MonoBehaviour
{
    // A array of Image components representing the hearts in the UI
    public Image[] hearts;

    // The sprites for full and empty hearts
    public Sprite fullHeart;
    public Sprite emptyHeart;

    
    public void UpdateHealth(int currentHealth)
    {
        // Check each heart in the array
        for (int i = 0; i < hearts.Length; i++)
        {
            // If the index is less than current health -> Volles Herz
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            // else -> leeres Herz
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}