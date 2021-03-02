using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnLogic : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Transform[] Doors;
    private int EnemyToSpawn;
    private int SpawnedEnemyCount;
    private int TimeToSpawn;
    void Start()
    {
        SetEnemyCount();
        StartCoroutine(SpawnEnemies());
    }

    private void SetEnemyCount()
    {
        switch (GameSettings.Difficulty)
        {
            case 0:
                {
                    EnemyToSpawn = 15;
                    TimeToSpawn = 15;
                    break;
                }
            case 1:
                {
                    EnemyToSpawn = 25;
                    TimeToSpawn = 7;
                    break;
                }
            case 2:
                {
                    EnemyToSpawn = 40;
                    TimeToSpawn = 4;
                    break;
                }
        }
    }

    void Update()
    {
        
    }

    private void EndLogic()
    {
        return;
    }

    private IEnumerator SpawnEnemies()
    {
        while (SpawnedEnemyCount != EnemyToSpawn)
        {
            Transform selectedDoor = Doors[UnityEngine.Random.Range(0, Doors.Length)];
            GameObject enemy = Instantiate(EnemyPrefab);
            enemy.transform.parent = transform;
            enemy.transform.position = selectedDoor.position;
            enemy.transform.localPosition = new Vector3(enemy.transform.localPosition.x, -2.502f);
            selectedDoor.localEulerAngles = new Vector3(0, 57, 0);
            yield return new WaitForSeconds(1);
            selectedDoor.localEulerAngles = new Vector3(0, 0, 0);
            SpawnedEnemyCount++;
            yield return new WaitForSeconds(TimeToSpawn);
        }
    }
}
