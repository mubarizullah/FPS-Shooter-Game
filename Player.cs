using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
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
  Vector2 restValue ;
   public bool isRunning = false;
   public float playerHealth = 500;
   GunController gun;
   [SerializeField]
   float jumpForce = 1f;

   
  
   public void Awake()
   {
      //Enabling Input Action 
      playerMovementInputActions = new PlayerMovement();
      playerMovementInputActions.Player.Jump.performed += Jump;
      playerMovementInputActions.Player.Movement.Enable();
      
      controller = GetComponent<CharacterController>();
      gun = GameObject.FindWithTag("Gun").GetComponent<GunController>();

   }
   

   void Update()
{
  restValue = new Vector2(0f,0f);
  if(movValue != restValue)
  {
    gun?.weoponAnimator.SetBool("walking", true);
  }
  else if(restValue == movValue)
  {
    gun.weoponAnimator.SetBool("walking",false);
  }



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


public void Jump(InputAction.CallbackContext context)
{
  Debug.Log("player should jump");
  mov = transform.up * jumpForce ;
}

}
