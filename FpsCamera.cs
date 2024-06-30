using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCamera : MonoBehaviour
{
   public PlayerMovement InputActions;
   public float verticalSensitivity = 2f;
   public float horizontalSentivity = 2f;
   public Transform playerHead;
   public Transform playerTransform;
   float yawForHorizontalRot;
   float pitchForVerticalRot;
   Quaternion cameraQuaternionRot;
   public Vector2 delta;
   public float cameraRange;

   void Awake()
   {
    InputActions = new PlayerMovement();
    InputActions.Player.Look.Enable();
    //Cursor.lockState = CursorLockMode.Locked;
   }

   void Update()
   {
    delta = InputActions.Player.Look.ReadValue<Vector2>();

    float deltaX = delta.x * horizontalSentivity;
    float deltaY = delta.y * verticalSensitivity;

    //updating horizontal rotation
    yawForHorizontalRot += deltaX;
    pitchForVerticalRot = Mathf.Clamp(pitchForVerticalRot - deltaY , -cameraRange , cameraRange);
    cameraQuaternionRot = Quaternion.Euler(pitchForVerticalRot , yawForHorizontalRot , 0f);
    transform.rotation = cameraQuaternionRot;
    playerTransform.rotation = Quaternion.Euler(0f, yawForHorizontalRot , 0f);
    transform.position = playerHead.position;
   }
}
