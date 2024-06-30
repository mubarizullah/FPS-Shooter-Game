using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunHandling : MonoBehaviour
{
  public int gunNum;

void Start()
{
  //EquipGun(0);
}

void Update()
{ 
  EquipGun(gunNum);
}

void EquipGun(int gunIndex)
{
//this line automatically iterate between all the gameObjects this script is attached to
//we dont have to make any array or use GetComponentInChildren()
int i = 0;  
  foreach (Transform gunChildren in transform)    
  {

    //I start looping with 0
    
    if (i == gunIndex)
    {
        gunChildren.gameObject.SetActive(true);
    }
    else 
    {
        gunChildren.gameObject.SetActive(false);
    }

    i++;

  }
}
public void SelectAK47() // button event for gunSelection
{
  gunNum = 0;
  Debug.Log("h");
}
public void SelectAUG() // button event for gunSelection
{
  gunNum = 1;
  Debug.Log("h");
}

}
