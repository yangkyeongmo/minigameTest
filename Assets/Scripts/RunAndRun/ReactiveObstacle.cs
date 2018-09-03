using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveObstacle : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Character"))
        {
            transform.parent.Find("Sprite").localScale = new Vector3(12.0f, 12.0f, 1.0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Character"))
        {
            transform.parent.Find("Sprite").localScale = new Vector3(6.0f, 6.0f, 1.0f);
        }
    }
}
