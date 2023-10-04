using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxCoin : MonoBehaviour
{
    public Animator QuestionBoxCoinAnimator;
    public Animator CoinAnimator;

    public int hitted = 1;

    void OnCollisionEnter2D(Collision2D col)
    {
        hitted--;
        if (hitted == 0)
        {
            QuestionBoxCoinAnimator.SetTrigger("Hit");
            CoinAnimator.SetTrigger("Hit");

        }
    }


}
