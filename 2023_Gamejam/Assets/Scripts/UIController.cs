using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject gameOverScreen;

    private GameObject damageIndicatorScreen;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        damageIndicatorScreen = GameObject.Find("DamageIndicator");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        damageIndicatorScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void startGame()
    {
        damageIndicatorScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }
}
