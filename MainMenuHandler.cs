using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
   public GameObject ExitConfirmation;
   public GameObject MainMenuUI;
   public void Play()
   {
    SceneManager.LoadScene("Level 1");
   }

   public void Exit()
   {
    ExitConfirmation.SetActive(true);
    MainMenuUI.SetActive(false);
   }

   public void GameExitYes()
   {
      Application.Quit();
      Debug.Log("Game is quiting");
   }
   
   public void GameExitNo()
   {
    ExitConfirmation.SetActive(false);
    MainMenuUI.SetActive(true);
   }

   public void Reward()
   {
    
   }

   public void RateUs()
   {
    
   }

   public void MoreGames()
   {
    
   }

   public void PrivacyPolicy()
   {
    
   }

   public void Setting()
   {
    
   }

   public void UnlockAllLevels()
   {
    
   }

   public void UnlockAllGuns()
   {
    
   }

   public void Acheivments()
   {
    
   }

   public void NoAdd()
   {
    
   }

   public void GunSelection()
   {
    
   }

   
}
