using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool _startAttack = false;

    [Header("적 공격 시간")]
    [SerializeField] private float _attackAnimationTime = 2f;
    private float _attackTimeimeElapsed = 2f;

    [Header("적 총알 프리팹")]
    [SerializeField] private GameObject _bulletPrefab;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_startAttack)
        {
            _attackTimeimeElapsed += Time.deltaTime;

            if(_attackTimeimeElapsed >= _attackAnimationTime)
            {
                _animator.SetTrigger("Attack");
                Fire();
                _attackTimeimeElapsed = 0f;
            }
        }
    }

    public void StartAttack()
    {
        _startAttack = true;
    }

    private void Fire()
    {

    }
}
