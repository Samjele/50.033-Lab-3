using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSound : MonoBehaviour
{
    public Animator CoinAnimator;
    public AudioSource coinAudio;
    // public AudioClip coinSound;
    // Start is called before the first frame update
    void PlayCoinSound()
    {
        // play jump sound
        coinAudio.PlayOneShot(coinAudio.clip);
    }
}
