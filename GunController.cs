using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GunController : MonoBehaviour
{
    [Header("WEAPON SPECS")]
    public int currentAmmo;
    [Tooltip("Magzine Size")]
    public int fullMagzineAmmo = 60;
    [Tooltip("Maximum distance for shooting.")]
    public float fireRange = 300f;
    [Tooltip("More Fire Rate the less time it will take for the next fire.")]
    public float FireRate = 15f;
     [Tooltip("Total number of bullets that a gun holds.")]
    public int totalAmmo =120;
    [Tooltip("The time weopon will take to Reload")]
    public float waitForReloading = 2.3f;
    [Tooltip("Damage the gun gives to enemy")]
    public float damageOfGun = 10;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;
    ////////////////////////////////////////////
    [Header("LAYERS FOR FIRE DETECTION")]
    [Tooltip("Layer of Enemy")]
    public LayerMask enemyMaskLayer;
    [Tooltip("Layer of my enviroment e.g walls and floor")]
    public LayerMask enviromentMask;
    /////////////////////////////////////////////
    [Header("GAMEOBJECTS REFRENCE")]
    [Tooltip("GameObject VFX or particle system that represent effect of bullet on Walls or Floor")]
    public GameObject bulletEffectOnWalls;
    [Tooltip("GameObject VFX or particle system that represent blood effect of bullet on Enemy")]
    public GameObject bulletEffectOnEnemy;
    [Tooltip("Refrence to the GameObject from where shooting ray has to be generated.")]
    public GameObject eyesCamera;
    public GameObject muzzleFlashForAssault;
    public Transform shootingPointOfAssault;
    //////////////////////////////////////////////
    [Header("COMPONENTS REFERNCE")]
    [Tooltip("Animator controler for shooting and reloading")]
    public Animator weoponAnimator;
    public AudioManager audioManager;
    public EnemyController enemyController;
    public PlayerMovement InputSystem;

    void Awake()
    {
        weoponAnimator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
        InputSystem = new PlayerMovement();
        InputSystem.Player.Shoot.Enable();
        weoponAnimator.Play("EquipGun");
    }
    void Start()
    {
      currentAmmo = fullMagzineAmmo;
      weoponAnimator.SetTrigger("toIdle");
    }
    
   ///////////////////////////////////////////////////---UPDATE---//////////////////////////////////////////////////
   
   void Update()
   {
      bool enterButtonClicked = InputSystem.Player.Shoot.IsPressed();

    if (isReloading)
    {
        return;
    }
                                                   //SHOOTING SCENARIO
     if (Time.time >= nextTimeToFire && currentAmmo != 0 && enterButtonClicked) 
     {
      nextTimeToFire = Time.time +1/FireRate;
      Shoot();
     }
     else
     {
        weoponAnimator.SetBool("IsFiring",false);
     }    
   }
   
    //////////////////////////////////////////////////////---SHOOTING---////////////////////////////////////////////////////////////
   
   public void Shoot()
   {
     weoponAnimator.SetBool("IsFiring",true);
     currentAmmo--;
     if (totalAmmo > 0)
     {
      totalAmmo --;
     }
     if (currentAmmo <= 0 && totalAmmo >= 0 && totalAmmo != 0)
     {
        StartCoroutine(ReLoading());
     }
                        ///////////////////////////---SHOOTING ENVIROMENT---///////////////////////
     
     if (Physics.Raycast(eyesCamera.transform.position,eyesCamera.transform.forward, out RaycastHit hitInfoWall , fireRange , enviromentMask))
     {
        GameObject effectGO = Instantiate(bulletEffectOnWalls,hitInfoWall.point,Quaternion.LookRotation(hitInfoWall.normal));
        Destroy(effectGO,2f);
     }

                         ///////////////////////////---SHOOTING ENEMY---////////////////////////////
     
     if (Physics.Raycast(eyesCamera.transform.position,eyesCamera.transform.forward, out RaycastHit hitInfo, fireRange , enemyMaskLayer)  )
     {

        GameObject effectGO2 = Instantiate(bulletEffectOnEnemy,hitInfo.point,Quaternion.LookRotation(hitInfo.normal));
        Destroy(effectGO2,3f);
        EnemyController enemy = hitInfo.collider.gameObject.GetComponent<EnemyController>();
        enemy?.HealthDeduction(damageOfGun);
        enemy?.enemyAnimator.SetBool("Damage",true);
        
     }
   }

   ///////////////////////////////////////////////---RELOADING---////////////////////////////////////////////////////////////
   public IEnumerator ReLoading()
   {
    weoponAnimator.SetBool("IsFiring",false);
    isReloading = true;
    weoponAnimator.Play("Reload",0,0f);
    yield return new WaitForSeconds(waitForReloading);
    
    if (totalAmmo < fullMagzineAmmo)
    {
      currentAmmo = totalAmmo;
      totalAmmo = 0 ;
      isReloading = false;
    }
    else
    {
      currentAmmo = fullMagzineAmmo;
      totalAmmo -= fullMagzineAmmo;
      isReloading = false;
    }
    weoponAnimator.SetBool("IsFiring",true);
    yield return new WaitForSeconds(1f);
   }


////////////////////////////////////////////////---RELOADING FOR UI---///////////////////////////////////////////////


   public void Reloading()
   {
      StartCoroutine(OnClickReload());
   }
   public IEnumerator OnClickReload()
   {

    if (totalAmmo != 0)
    {
   weoponAnimator.SetBool("IsFiring",false);
   isReloading = true;
   weoponAnimator.Play("Reload",0,0f);
   yield return new WaitForSeconds(waitForReloading);
   int bulletsUsedInThisMagzine = fullMagzineAmmo - currentAmmo;
   currentAmmo += bulletsUsedInThisMagzine;
   totalAmmo -= bulletsUsedInThisMagzine;
   isReloading = false;
   weoponAnimator.SetBool("IsFiring",true);
    }
   }

///////////////////////////////////////////////////////---EVENTS---//////////////////////////////////////////////////////////////////
   public void ReloadSoundEventForAnimation()
   {
     audioManager.PlaySound("Reload");
   }


   
   public void DamageAnimationFalse()
   {
      enemyController.enemyAnimator.SetBool("Damage",false);
   }


   public void ShootEffectForAnimation()
   {
      GameObject muzzleForAssault = Instantiate(muzzleFlashForAssault,shootingPointOfAssault.position,Quaternion.identity);
      Destroy(muzzleForAssault,0.2f);
   }


   

//////////////////////////////////////---SOUNDS EVENTS FOR GUNS---////////////////////////////////////////////////////////
   public void SoundForAUG()
   {
     audioManager.PlaySound("AUG");
   }
   public void SoundFAMAS()
   {
    audioManager.PlaySound("Assault");
   }



 //////////////////////////////////////////////---GIZMOS---///////////////////////////////////////////////////////////////

      void OnDrawGizmos()
   {
    Gizmos.color = Color.green;
    Gizmos.DrawRay(eyesCamera.transform.position, transform.position*fireRange);
   }
   
}
