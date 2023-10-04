using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;

    public void Start()
    {
        gameObject.SetActive(false);
    }
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = "SCORE : " + score.ToString();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }


}
