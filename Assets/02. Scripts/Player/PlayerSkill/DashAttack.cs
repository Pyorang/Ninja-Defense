using UnityEngine;

public class DashAttack : Skill
{
    [Header("스킬 사거리")]
    [Space]
    [SerializeField] private float _distance = 1f;

    [Header("스킬 이펙트")]
    [Space]
    [SerializeField] private GameObject _skillEffect;

    [Header("적 레이어 설정")]
    [SerializeField] private int _enemyLayer = 7;

    private void Update()
    {
        if (Input.GetKeyDown(_pressButton) && !_isAttacking)
        {
            Execute();
        }
    }

    public override void Execute()
    {
        if(ComboManager.Instance.UseSkill())
        {
            _isAttacking = true;
            _playerMove.SetMovementLock(true);
            StartCoroutine(WaitAttackTime());

            _animator.SetTrigger("Skill1");

            Vector3 targetDirection = gameObject.GetComponent<SpriteRenderer>().flipX ? Vector3.left : Vector3.right;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, targetDirection, _distance);

            if(hits.Length > 1)
            {
                AudioManager.Instance.PlaySound("LeftShift", AudioType.SFX);
            }
            else
            {
                AudioManager.Instance.PlaySound("Z", AudioType.SFX);
            }

            foreach (RaycastHit2D hit in hits)
            {
                EnemyStat enemy = hit.transform.gameObject.GetComponent<EnemyStat>();

                if (enemy != null && enemy.gameObject.layer == _enemyLayer)
                {
                    enemy.GetHit();
                    Instantiate(_skillEffect, hit.transform.position, Quaternion.identity);
                }
            }

            transform.position += targetDirection * _distance;
        }
    }
}
