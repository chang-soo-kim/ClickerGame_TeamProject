using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChanageSceneManager : MonoBehaviour
{

    public void ONRESTARTGAME()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }

}
