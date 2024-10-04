using System.Collections.Generic;
using UnityEngine;

public class MainEnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _spawners;

    [SerializeField] private float _repeatRate = 2.0f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0.0f, _repeatRate);
    }

    private void SpawnEnemy()
    {
        int randomSpawnerNumber = Random.Range(0, _spawners.Count);
        _spawners[randomSpawnerNumber].Spawn();
    }
}
