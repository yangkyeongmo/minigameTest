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
    public Text gameStartText;
    public GameObject gameStartFade;

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
        gameStartText.text = "";
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.isGameStart)
        {
            timer = gc.GetTimer();
            if (!chp.GetIsCharacterStopped() && !GameControl.isGameEnd)
            {
                runmeter += Time.deltaTime * runSpeedByMeterPerSec;
            }
            debugText.text = Time.time.ToString();
            timerText.text = ((int)timer).ToString() + ":" + ((int)((timer % 1) * 100)).ToString();
            if (timer > 0)
            {
                runMeterText.text = runmeter.ToString("N2") + "m";
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
        else
        {
        }
    }

    public void InstantiateText()
    {
        isCreatedText = true;
        Instantiate(timerDecreaseText, new Vector3(timerText.transform.position.x - 20.0f, timerText.transform.position.y, 0.0f), Quaternion.identity).rectTransform.SetParent(GameObject.Find("UICanvas").transform);
    }

    private IEnumerator GameStart()
    {
        gameStartFade.gameObject.SetActive(true);
        for(int i=3; i>=0; i--)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            gameStartText.gameObject.SetActive(true);
            if (i == 0)
            {
                gameStartText.text = "START";
                yield return new WaitForSecondsRealtime(1.0f);
            }
            else
            {
                gameStartText.text = i.ToString();
                yield return new WaitForSecondsRealtime(0.5f);
            }
            gameStartText.gameObject.SetActive(false);
        }
        gameStartFade.gameObject.SetActive(false);
        GameControl.isGameStart = true;
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
