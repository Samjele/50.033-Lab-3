using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCoin : MonoBehaviour
{
    public Animator BrickCoinAnimator;
    public Animator CoinAnimator;

    public int hitted = 1;

    void OnCollisionEnter2D(Collision2D col)
    {
        hitted--;
        if (hitted == 0)
        {
            BrickCoinAnimator.SetTrigger("Hit");
            CoinAnimator.SetTrigger("Hit");

        }
        if (hitted < 0)
        {
            BrickCoinAnimator.SetTrigger("Hit");
        }
    }

}
