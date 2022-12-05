using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Reference to the current Player object to get hit point fields
    // Will be set programmatically, instead of through the Unity Editor, so it is hidden in the Inspector window
    [HideInInspector]
    public Player character;

    // For convenience, a direct reference to the health bar meter; set through the Unity Editor
    public Sprite fullSprite;
    public Sprite emptySprite;
    public Sprite halfSprite;
    
    private Image image;

    // Update is called once per frame
    private void Start()
    {
        // get references to game object components so they don't have to be grabbed each time they are needed
        if (character != null)
        {
            image = GetComponentInChildren<Image>();
        }
        
    }
    void Update()
    {
        if (character != null)
        {
            if (character.hitPoints == character.maxHitPoints)
            {
                image.sprite = fullSprite;
            }
            else if(character.hitPoints % 2 != 0)
            {
                image.sprite = halfSprite;
            }
            else
            {
                image.sprite = emptySprite;
            }
            //heartImage.fillAmount = character.hitPoints / character.maxHitPoints;
            
        }
    }
}