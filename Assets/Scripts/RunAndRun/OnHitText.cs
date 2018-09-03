using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnHitText : ScriptedUIText {

    public float fadeTime;
    public List<string> hitSound;

    private Text thisText;
    private Text thisArrowText;

    // Use this for initialization
    void Start()
    {
        thisText = transform.Find("HitText").GetComponent<Text>();
        thisText.text = hitSound[Random.Range(0, hitSound.Count)];
        thisArrowText = transform.Find("HitTextArrow").GetComponent<Text>();
    }
	
	void Update()
    {
        FadeText(thisText.color, fadeTime, Color.black, Color.white);
        FadeText(thisArrowText.color, fadeTime, Color.black, Color.white);
    }
}
