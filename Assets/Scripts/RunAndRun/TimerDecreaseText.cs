using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDecreaseText : ScriptedUIText {

    public float fadeTime;
    public float upSpeed;
    
    private float colorFadeTime;

    Text thisText;

    private void Start()
    {
        thisText = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        FadeText(thisText.color, fadeTime, Color.black, Color.white, Vector3.up, upSpeed);
    }
}
