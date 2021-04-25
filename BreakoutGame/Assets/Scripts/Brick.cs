using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points;
    public int hitsNeeded;
    public Sprite hitSprite;
    public Sprite secondHitSprite;
    [SerializeField] private SoundManager sm;

    public int Length { get; internal set; }

    public void HitBrick()
    {
        hitsNeeded--;
        sm.PlaySound(0);
        if (hitsNeeded >= 2) GetComponent<SpriteRenderer>().sprite = hitSprite;
        if (hitsNeeded >= 1) GetComponent<SpriteRenderer>().sprite = secondHitSprite;
    }

}
