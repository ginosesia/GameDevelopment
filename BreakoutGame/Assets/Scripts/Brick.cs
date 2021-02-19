using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points;
    public int hitsNeeded;
    public Sprite hitSprite;
    public Sprite secondHitSprite;

    public void HitBrick()
    {
        hitsNeeded--;
        if (hitsNeeded >= 2) GetComponent<SpriteRenderer>().sprite = hitSprite;
        if (hitsNeeded >= 1) GetComponent<SpriteRenderer>().sprite = secondHitSprite;
    }

}
