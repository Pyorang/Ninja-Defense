using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool _startAttack = false;

    [Header("적 공격 시간")]
    [SerializeField] private float _attackAnimationTime = 2f;
    private float _attackTimeimeElapsed = 2f;

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

    private void OnDisable()
    {
        _startAttack = false;
    }

    public void StartAttack()
    {
        _startAttack = true;
    }

    private void Fire()
    {

    }
}
