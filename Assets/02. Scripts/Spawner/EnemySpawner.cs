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

    private void Start()
    {
        _currentSpawnCoolTime = Random.Range(_minSpawnCoolTime, _maxSpawnCoolTime);
    }
    private void Update()
    {
        _currentSpawnCoolTime -= Time.deltaTime;

        if (_currentSpawnCoolTime <= 0)
        {
            _currentSpawnCoolTime = Random.Range(_minSpawnCoolTime, _maxSpawnCoolTime);

            GameObject enemy = EnemyFactory.Instance.GetObject(transform.position);

            EnemyAttack enemyAttack = enemy.GetComponent<EnemyAttack>();
            enemyAttack.enabled = true;

            EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
            enemyMove.enabled = true;

            enemyMove.LockTarget(_target);
        }
    }

}
