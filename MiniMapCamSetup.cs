using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamSetup : MonoBehaviour
{
   public Transform playerTransform;


   public void Update()
   {
    transform.localPosition = new Vector3(0,20f,0);
   }
}
