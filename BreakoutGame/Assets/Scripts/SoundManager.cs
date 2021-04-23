using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager sm;
    private AudioSource audioSource;
    [SerializeField] private OptionsStates optionsStates;
    [SerializeField] private AudioClip PaddleHit;
    [SerializeField] private AudioClip WallHit;
    [SerializeField] private AudioClip OutOfBoundsHit;
    [SerializeField] private AudioClip BrickHit;
    [SerializeField] private AudioClip PowerUp;


    // Start is called before the first frame update
    void Start()
    {
        sm = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int type)
    {
        if (optionsStates.SoundState)
        {
            if (type == 0) audioSource.PlayOneShot(BrickHit);
            if (type == 1) audioSource.PlayOneShot(OutOfBoundsHit);
            if (type == 2) audioSource.PlayOneShot(PowerUp);
            if (type == 3) audioSource.PlayOneShot(PaddleHit);
        }
    }
}
