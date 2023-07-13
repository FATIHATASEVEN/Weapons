using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float killcount;
    public Text killcountText;

    public GameObject pauseMenuUI;
    public GameObject winLevelUI;

    private bool gameispaused = false;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameispaused==true)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        gameispaused = true;
    }
    void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;
        gameispaused = false;

    }
    public void WinLevel()
    {
        winLevelUI.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
       
    }
    public void AppQuit()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void AddKill()
    {
        killcount++;
        killcountText.text = "KILL  " + killcount;
    }
}
