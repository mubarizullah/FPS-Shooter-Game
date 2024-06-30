using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1Handle : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int totalNumberOfEnemies = 4;
  
  void Start()
  {
   StartCoroutine(EnemyGenartor());
   EnemyController.OnEnemyDieEvent+= EnemyNumberDeduction;
  }
  void Update()
  {
    if (totalNumberOfEnemies == 0)
    {
      //OnMissionComplete?.Invoke();
    }
  }
   
   IEnumerator EnemyGenartor()
   {
    for (int i = 0; i < 4; i++)
    {
        SpawningEnemies(i);
        yield return new  WaitForSeconds(15f);
    }
   }

    void SpawningEnemies(int pointOfInstantiating)
   {
    Debug.Log("Spawning another enemy after 10 seconds");
    Instantiate(enemyPrefab,spawnPoints[pointOfInstantiating].position,Quaternion.identity);
   }
   
   public void EnemyNumberDeduction()
   {
    totalNumberOfEnemies --;
   }

   public delegate void MissionComplete();
   public static event MissionComplete OnMissionComplete;
}
