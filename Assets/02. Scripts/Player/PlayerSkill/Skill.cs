using System.Collections;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [Header("키보드 키 설정")]
    [Space]
    [SerializeField] protected KeyCode _pressButton;

    [Header("재사용 대기 시간")]
    [Space]
    protected static bool _isAttacking = false;
    [SerializeField] private float _waitTimeToControl = 1f;

    protected PlayerMove _playerMove;
    protected Animator _animator;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(_pressButton) && !_isAttacking)
        {
            _isAttacking = true;
            _playerMove.SetMovementLock(true);
            Execute();
            StartCoroutine(WaitAttackTime());
        }
    }

    private IEnumerator WaitAttackTime()
    {
        yield return new WaitForSeconds(_waitTimeToControl);
        _isAttacking = false;
        _playerMove.SetMovementLock(false);
    }

    public abstract void Execute();
}
