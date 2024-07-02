using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
   [Header("Enemy Attributes")]
   public float Health = 100f;
   /////////////////////////////
   [Header("Components Required")]
   [Tooltip("Make sure to configure it before assigning according to enemy")]
   public Animator enemyAnimator;
   public  NavMeshAgent enemyNavAgent;
   /////////////////////////
   [Header("GO Refrence")]
   [Tooltip("Add GameObject which enemy wil chase.")]
   Transform player;
   [Tooltip("Pistol one transform from where muzzle flash will be given out.")]
   public Transform rightFlashPoint;
   [Tooltip("Pistol one transform from where muzzle flash will be given out.")]
   public Transform leftFlashPoint;
   [Tooltip("Muzzle flash of ContractKiller Pistol")]
   public GameObject muzzleFlashPistol;
   [Header("Others")]
   AudioManager audioManager;
   public LayerMask playerMask;


   public CapsuleCollider capCollider;
   public float shootingDuration = 3f;
   public float damageToPlayer = 20;
   Player playerScript;
   
   
   float time = 0;   

   void Start()
   {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    audioManager = FindObjectOfType<AudioManager>();
    MovingToPlayer();
    enemyAnimator.SetBool("Firing",false);
    capCollider = GetComponent<CapsuleCollider>();
    playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();

    

   }

   void Update()
   {
    
    float distBetweenPlayerEnemy = Vector3.Distance(player.position,transform.position);
    if (distBetweenPlayerEnemy <= 5f)
    {
        StartCoroutine(EnemyInFrontOfPlayer());
    }
    else if(distBetweenPlayerEnemy >= 5f)
    {
        MovingToPlayer();
    }
    
   }

   void MovingToPlayer()
   {
    enemyNavAgent.isStopped = false;
    enemyAnimator.SetBool("Firing",false);
    enemyAnimator.SetBool("IsWalking",true);
    enemyNavAgent.SetDestination(player.position);
   }

   IEnumerator EnemyInFrontOfPlayer()
{
    // Ensure player detection and prevent null reference errors
    if (player == null)
    {
        Debug.LogError("EnemyInFrontOfPlayer: Player reference not set!");
        yield break;
    }

    // Rotate towards player
    transform.LookAt(player.position);

    // Stop movement and adjust animator (optional)
    enemyNavAgent.isStopped = true;
    enemyAnimator.SetBool("IsWalking", false);

    while (Physics.Raycast(transform.position, transform.forward, 10f, playerMask))
    {
         time = 0f;
        enemyAnimator.SetBool("Firing", true);

        
        while (time < 3f)
        {
            time += Time.deltaTime;
            yield return null; 
        }

        enemyAnimator.SetBool("Firing", false);
        yield return new WaitForSeconds(3f); 
        break; 
    }
}   
    
     


   //////Method to serve as events in animation for muzzle flash and fire sound 
   public void LeftPistolFunctions()
   {
    GameObject muzzleLeft = Instantiate(muzzleFlashPistol,leftFlashPoint.position,Quaternion.identity);
    Destroy(muzzleLeft,0.1f);
    audioManager.PlaySound("Pistol");
   }

   public void RightPistolFuntions()
   {
    GameObject muzzleRight = Instantiate(muzzleFlashPistol,rightFlashPoint.position,Quaternion.identity);
    Destroy(muzzleRight,0.1f);
    audioManager.PlaySound("Pistol");
   }

   public void HealthDeduction(float damage)
   {
    enemyAnimator.SetBool("Damage",true);
    Health -= damage;
    Debug.Log("health is been deducted");
    if (Health <=0)
    {
        EnemyDied();
    }
   }
   public void DamageAnimationFalse()
   {
     enemyAnimator.SetBool("Damage",false);
   }

   public delegate void EnemyDieDelegate();
   public static event EnemyDieDelegate OnEnemyDieEvent;

   public void EnemyDied()
   {
    // dying logic
    Debug.Log("Enemy has died");
    enemyAnimator.Play("Death");    
    capCollider.enabled = false;
    Destroy(gameObject , 6f);
    OnEnemyDieEvent?.Invoke();
   }

   public void PlayerHealthDeduction()
   {
   Debug.Log("method working");
   playerScript?.HealthDuduction(damageToPlayer);

    ChangingBloodColor(255);
    
   }


   public void BloodVfxDisable()
   {
    
    ChangingBloodColor(0);
   }

   void ChangingBloodColor(float alpha)
   {
    UnityEngine.UI.Image bloodimage = GameObject.FindWithTag("BloodUI").GetComponent<UnityEngine.UI.Image>();
    bloodimage.color = new Color(bloodimage.color.r,bloodimage.color.g,bloodimage.color.b,alpha);
   }
   
   

}
   

