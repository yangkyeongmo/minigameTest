using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextControl : MonoBehaviour {

    public float runSpeedByMeterPerSec;

    public Text debugText;
    public Text timerText;
    public Text runMeterText;
    public Text timerDecreaseText;
    public GameObject onHitText;

    private GameControl gc;
    private Character ch;
    private CharacterPhysics chp;
    private float timer;
    private float runmeter = 0.0f;
    private bool isCreatedText = false;
    private RectTransform canvasRect;

	// Use this for initialization
	void Start () {
        gc = GetComponent<GameControl>();
        ch = GameObject.Find("Character").GetComponent<Character>();
        chp = GameObject.Find("Character").transform.Find("Sprite").GetComponent<CharacterPhysics>();
        canvasRect = GameObject.Find("UICanvas").GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        timer = gc.GetTimer();
        if(!chp.GetIsCharacterStopped() && !GameControl.isGameEnd){
            runmeter += Time.deltaTime * runSpeedByMeterPerSec;
        }
        debugText.text = Time.time.ToString();
        timerText.text = "TIMER: " + timer.ToString("N2");
        if (timer > 0)
        {
            runMeterText.text = "RUN: " + runmeter.ToString("N2") + "m";
        }
        if (ch.isHit && !isCreatedText)
        {
            InstantiateText();
        }
        if (!ch.isHit && isCreatedText)
        {
            isCreatedText = false;
        }
    }

    public void InstantiateText()
    {
        isCreatedText = true;
        Instantiate(timerDecreaseText, new Vector3(timerText.transform.position.x - 20.0f, timerText.transform.position.y, 0.0f), Quaternion.identity).rectTransform.SetParent(GameObject.Find("UICanvas").transform);
        Instantiate(onHitText, canvasRect.rect.center +  new Vector2(765.0f, 322.0f), Quaternion.identity).transform.SetParent(GameObject.Find("UICanvas").transform);
    }

    public bool GetIsCreatedText()
    {
        return isCreatedText;
    }
    public void SetIsCreatedText(bool setBool)
    {
        isCreatedText = setBool;
    }
    public float GetRunMeter(){
        return runmeter;
    }
}
