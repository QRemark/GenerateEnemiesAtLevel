using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Vector3 _speed;

    private ObjectPool<Enemy> _enemyPool;

    private int _poolCapacity = 10;
    private int _poolMaxSize = 15;

    private void Awake()//сделать коллекцию спавнеров
    {
        _enemyPool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: SubscribeOnEnemy,
            actionOnRelease: UnsubscribeOnEnemy,
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private Enemy GetEnemy()
    {
        Enemy enemy = _enemyPool.Get();

        enemy.Init();

        return enemy;
    }

    public void Spawn()
    {
        Enemy enemy = GetEnemy();
        
        enemy.transform.position = _startPoint.position;

        enemy.ResetSpeed();

        enemy.AddSpeed(_speed);
    }

    private void SubscribeOnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.EndWay += ReturnEnemyInPool;
    }

    private void UnsubscribeOnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.EndWay -= ReturnEnemyInPool;
    }

    private void ReturnEnemyInPool(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }
}
