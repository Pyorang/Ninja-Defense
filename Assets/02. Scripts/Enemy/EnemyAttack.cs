using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool _startAttack = false;

    [Header("적 공격 시간")]
    [SerializeField] private float _attackAnimationTime = 2f;
    private float _attackTimeimeElapsed = 2f;

    private Hack _target;
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
                _attackTimeimeElapsed = 0f;
            }
        }
    }

    private void OnDisable()
    {
        _startAttack = false;
    }

    public void StartAttack(Hack target)
    {
        _startAttack = true;
        _target = target;
    }

    public void Fire()
    {
        if(_target != null)
        {
            AudioManager.Instance.PlaySound("Fire", AudioType.SFX);
            _target.GetDamage(1f);
        }
    }
}
