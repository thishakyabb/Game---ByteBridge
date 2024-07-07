using System;
using System.Collections;
using UnityEngine;

// this class is only to handle blinking. spawning and despawning is taken care of by other classes
public class WarningX: MonoBehaviour
{
public SpriteRenderer warningSprite; // Reference to the warning SpriteRenderer
    public float totalTime = 10f; // Total time for the timer
    public float minBlinkInterval = 0.1f; // Minimum interval between blinks
    public float maxBlinkInterval = 1f; // Maximum interval between blinks

    private float timer;
    private bool isBlinking;

    void Start()
    {
        timer = totalTime;
        StartCoroutine(Blink());
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            SetTransparency(0); // Make the sprite completely transparent when the timer is up
            StopAllCoroutines(); // Stop blinking when the timer is up
        }
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            ToggleTransparency();
            float blinkInterval = Mathf.Lerp(minBlinkInterval, maxBlinkInterval, timer / totalTime);
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private void ToggleTransparency()
    {
        Color color = warningSprite.color;
        color.a = (color.a == 1f) ? 0f : 1f; // Toggle between fully opaque and fully transparent
        warningSprite.color = color;
    }

    private void SetTransparency(float alpha)
    {
        Color color = warningSprite.color;
        color.a = alpha;
        warningSprite.color = color;
    }
}