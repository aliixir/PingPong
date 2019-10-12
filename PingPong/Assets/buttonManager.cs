using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class buttonManager : MonoBehaviour
{

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void restartDuringGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void exitGame() {
      #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
      #else
        Application.Quit();
      #endif
    }
}
