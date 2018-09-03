using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour {

    public bool mine;

    public Sprite[] emptyTextures;
    public Sprite mineTexture;
    public Sprite markTexture;
    public Sprite mistakeTexture;
    public Sprite defaultTexture;

    private SpriteRenderer sr;
    private EventControl ec;
    private bool marked = false;
    private int x;
    private int y;
    private Vector3 mousePosition;

    // Use this for initialization
    void Awake () {
        sr = GetComponent<SpriteRenderer>();
        ec = GameObject.Find("Grid").GetComponent<EventControl>();
        //Random gerneration of mine
        mine = Random.value < 0.15;
        x = (int)transform.position.x;
        y = (int)transform.position.y;
        GridControl.elements[x, y] = this;
        if (mine)
        {
            ec.MineCountUp();
        }
	}

    void Start(){
        //sr.size = new Vector2(0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update ()
    {
        if (ec.GetTimer() > 0 && ec.GetBugCount() < ec.maxMineCount)
        {
            if (Input.GetMouseButtonUp(0) && !marked)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePosition.x > x - 0.5f && mousePosition.x < x + 0.5f && mousePosition.y > y - 0.5f && mousePosition.y < y + 0.5f)
                {
                    OnLeftMouseButtonClicked();
                }
            }
            if (Input.GetMouseButtonUp(1) && IsCovered())
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePosition.x > x - 0.5f && mousePosition.x < x + 0.5f && mousePosition.y > y - 0.5f && mousePosition.y < y + 0.5f)
                {
                    OnRightMouseButtonClicked();
                }
            }
        }
    }

    private void OnLeftMouseButtonClicked()
    {
        if (mine)
        {
            if (ec.GetTimer() > 0)
            {
                ec.TimerModify(-2.0f);
            }
            sr.sprite = mistakeTexture;
        }
        else
        {
            LoadTexture(GridControl.AdjacentMines(x, y));

            GridControl.FFuncover(x, y, new bool[GridControl.w, GridControl.h]);

            if (GridControl.IsFinished())
            {
                print("you win");
            }
        }
    }

    private void OnRightMouseButtonClicked()
    {
        if (!marked)
        {
            if (mine)
            {
                ec.BugCountUp();
            }
            sr.sprite = markTexture;
            marked = true;
        }
        else
        {
            if (mine)
            {
                ec.BugCountDown();
            }
            sr.sprite = defaultTexture;
            marked = false;
        }
        
    }

    public void LoadTexture(int adjacentCount)
    {
        if (mine)
        {
            sr.sprite = mineTexture;
        }
        else
        {
            sr.sprite = emptyTextures[adjacentCount];
        }
    }

    public bool IsCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.name == "mine_button" || GetComponent<SpriteRenderer>().sprite.name == "mine_check";
    }
}
