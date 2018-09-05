using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPhysics : MonoBehaviour {

    public float jumpFactor;
    public bool isOnGround = false;

    private Rigidbody2D rb;
    private Character character;
    private GameControl gc;
    private TerrainOverlapControl toc;
    private Quaternion startQuat;
    private GameObject collidedTerrain;
    private bool isCharacterStopped = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        character = transform.parent.GetComponent<Character>();
        gc = GameObject.Find("GameControl").GetComponent<GameControl>();
        toc = GameObject.Find("GameControl").GetComponent<TerrainOverlapControl>();
        startQuat = transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = startQuat;
        if (!GameControl.isGameEnd && GameControl.isGameStart)
        {
            if (isOnGround)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
                {
                    Jump();
                }
            }
            if (collidedTerrain != null && transform.position.y > collidedTerrain.transform.position.y + 5.0f)
            {
                toc.SetIsTerrainMoveAvailable(true);
            }
        }
	}

    private void Jump()
    {
        isOnGround = false;
        rb.velocity = new Vector2(0, 1.0f) * jumpFactor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collidedTerrain = collision.gameObject;
            if (!character.GetIsInvincible())
            {
                print("is Hit");
                gc.ModifyTimer(-1.0f);
                character.isHit = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Terrain"))
        {
            collidedTerrain = collision.gameObject;
            if (!character.GetIsInvincible())
            {
                print("is Hit");
                gc.ModifyTimer(-1.0f);
                character.isHit = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if (collision.transform.CompareTag("Terrain"))
        {
            isCharacterStopped = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Ground"))
        {
            isOnGround = false;
            collidedTerrain = null;
        }
        if(collision.transform.CompareTag("Terrain")){
            isCharacterStopped = false;
        }
    }

    public bool GetIsCharacterStopped(){
        return isCharacterStopped;
    }
}
