using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{

    public void StartTheDamnGame()
    {
        SceneManager.LoadScene("Game");
    }
}
