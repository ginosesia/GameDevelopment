using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points;
    public int hitsNeeded;
    public Sprite hitSprite;

    public void HitBrick()
    {
        hitsNeeded--;
        GetComponent<SpriteRenderer>().sprite = hitSprite;
    }

}
