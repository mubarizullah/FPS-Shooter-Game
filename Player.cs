using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour  
{
  
   private CharacterController controller;
   public PlayerMovement playerMovementInputActions;
   public float playerSpeed = 5f;
  Vector3 mov;
  Vector2 movValue;
   public bool isRunning = false;
   public float playerHealth = 500;
  
   public void Awake()
   {
      //Enabling Input Action 
      playerMovementInputActions = new PlayerMovement();
      playerMovementInputActions.Player.Movement.Enable();
      controller = GetComponent<CharacterController>();
   }
   

   void Update()
{

  if (isRunning)
  {
    playerSpeed = 15f;
  }
  else playerSpeed = 5f;
                                       
      MovementOfPlayer();

 if (playerHealth <= 0)
 {
  PlayerHasDied();
 }

}    
 void MovementOfPlayer()
    {
      movValue = playerMovementInputActions.Player.Movement.ReadValue<Vector2>();  //reading vector2 value from Movement Action which has my WASD bindings
       mov = transform.right * movValue.x + transform.forward * movValue.y;         // creating a vector3 to use in Character Controller's Move method
      controller.Move(mov * playerSpeed * Time.deltaTime); 
    }


public void Run()
    {
      if (isRunning)
      {
        isRunning = false;
      }
      else isRunning = true;
    }

public void HealthDuduction(float damage)
{
  playerHealth -= damage;
}

public delegate void PlayerDied();
public static event PlayerDied OnPlayerDied;

public void PlayerHasDied()
{
 OnPlayerDied?.Invoke();
}

}
