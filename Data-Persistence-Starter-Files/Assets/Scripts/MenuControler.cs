using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControler : MonoBehaviour
{
    private void Start()
    {
        scorePlayer.Instance.setBestScore();
    }


    public void newGameMenu()
    {
        scorePlayer.Instance.StartNewGame();
    }

    public void quitGame()
    {
        scorePlayer.Instance.Exit();
    }
}
