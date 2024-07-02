using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiControls : MonoBehaviour
{
   public GameObject PauseGameObject;
   public GameObject PlayUI;
   public GameObject MissionExit;
   public GameObject MissionFailPanel;
   public Slider healthSlider;
   public GameObject missionCompleted;
   public GameObject missionFailUIs;
   Player player;
  

   void Start()
   {
    Player.OnPlayerDied += MissionFail;
    player = FindAnyObjectByType<Player>();
    Mission1Handle.OnMissionComplete += MissionComplete;
   }

   void Update()
   {
    healthSlider.value = player.playerHealth;
   }
   public void Pause()
   {
    
      Time.timeScale = 0;
      PlayUI.SetActive(false);
      PauseGameObject.SetActive(true);
    
   }
    public void Resume()
    {
      Time.timeScale = 1;
      PlayUI.SetActive(true);
      PauseGameObject.SetActive(false);
    }

    public void Restart()
    {
      SceneManager.LoadScene("Level 1");
      Time.timeScale = 1;
    }

    public void Home()
    {
      MissionExit.SetActive(true);
      missionFailUIs.SetActive(false);
    }

    public void MissionExitYes()
    {
       SceneManager.LoadScene("MainMenu");
    }
     
    public void MissionExitNo()
    {
      MissionExit.SetActive(false);
      missionFailUIs.SetActive(true);
    } 
    
    public void MissionFail()
    {
      MissionFailPanel.SetActive(true);
      Time.timeScale = 0;
    }

    public void MissionComplete()
    {
      Time.timeScale = 0;
      missionCompleted.SetActive(true);
    }
   
   
}
