using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("타겟 설정")]
    [Space]
    [SerializeField] private GameObject _target;

    [Header("스폰 쿨타임")]
    [Space]
    private float _currentSpawnCoolTime;
    [SerializeField] private float _minSpawnCoolTime = 1f;
    [SerializeField] private float _maxSpawnCoolTime = 3f;

    [Header("스폰 1회 시 최대 스폰 횟수")]
    [Space]
    [SerializeField] private int _minSpawnCount = 1;
    [SerializeField] private int _maxSpawnCount = 3;

    [Header("한 마리 스폰당 간격 시간")]
    [Space]
    [SerializeField] private float _intervalCoolTime = 1f;
    private bool _isSpawning = false;

    private void Start()
    {
        _currentSpawnCoolTime = Random.Range(_minSpawnCoolTime, _maxSpawnCoolTime);
    }
    private void Update()
    {
        if(!_isSpawning)
        {
            _currentSpawnCoolTime -= Time.deltaTime;

            if (_currentSpawnCoolTime <= 0)
            {
                _currentSpawnCoolTime = Random.Range(_minSpawnCoolTime, _maxSpawnCoolTime);

                _isSpawning = true;
                StartCoroutine(SpawnMultipleEnemies());
            }
        }
    }

    private IEnumerator SpawnMultipleEnemies()
    {
        int spawnCount = Random.Range(_minSpawnCount, _maxSpawnCount + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject enemy = EnemyFactory.Instance.GetObject(transform.position);

            EnemyAttack enemyAttack = enemy.GetComponent<EnemyAttack>();
            enemyAttack.enabled = true;

            EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
            enemyMove.enabled = true;

            enemyMove.LockTarget(_target);

            yield return new WaitForSeconds(_intervalCoolTime);
        }

        _isSpawning = false;
    }

}
