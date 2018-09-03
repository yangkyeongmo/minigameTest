using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedUIText : MonoBehaviour
{
    private float colorFade;

    public void FadeText(Color targetColor, float fadeTime, Color startColor, Color endColor)
    {
        if (colorFade <= 1)
        {
            colorFade += Time.deltaTime / fadeTime;
            targetColor = Color.Lerp(startColor, endColor, colorFade);
        }
        if (targetColor == endColor)
        {
            Destroy(gameObject);
        }
    }

    public void FadeText(Color targetColor, float fadeTime, Color startColor, Color endColor, Vector3 moveDirection, float moveSpeed)
    {
        if (colorFade <= 1)
        {
            colorFade += Time.deltaTime / fadeTime;
            targetColor = Color.Lerp(startColor, endColor, colorFade);
            transform.position += moveDirection * moveSpeed;
        }
        if (targetColor == endColor)
        {
            Destroy(gameObject);
        }
    }
}
