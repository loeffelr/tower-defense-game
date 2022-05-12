using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GameController : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public GameObject moveTo;
    public float timeToSpawn;
    private float _currentTimeToSpawn;
    public int amountOfSpawns;
    public float speed;
    public List<Enemy> enemies;

    void Update()
    {
        if (_currentTimeToSpawn > 0)
        {
            _currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            if (amountOfSpawns > 0)
            {
                GameObject spawnedObject = SpawnObject();
                Enemy enemy = spawnedObject.GetComponent<Enemy>();
                enemies.Add(enemy);
                _currentTimeToSpawn = timeToSpawn;
                amountOfSpawns -= 1;
            }
        }

        foreach (Enemy enemy in enemies)
        {
            if (enemy)
            {
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position,
                    moveTo.transform.position, Time.deltaTime * speed);

                if (enemy.health <= 0)
                {
                    Destroy(enemy.gameObject);
                    enemies.Remove(enemy);
                }

                if (Vector3.Distance(enemy.transform.position, moveTo.transform.position) < 0.2)
                {
                    Debug.Log("Game Over");
                    UnityEditor.EditorApplication.isPlaying = false;
                }
            }
        }
    }

    private GameObject SpawnObject()
    {
        return Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
    }
}