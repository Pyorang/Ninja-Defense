using UnityEngine;

public class DashAttack : Skill
{
    [Header("스킬 사거리")]
    [Space]
    [SerializeField] private float _distance = 1f;

    [Header("스킬 이펙트")]
    [Space]
    [SerializeField] private GameObject _skillEffect;

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
            foreach (RaycastHit2D hit in hits)
            {
                EnemyStat enemy = hit.transform.gameObject.GetComponent<EnemyStat>();

                if (enemy != null)
                {
                    enemy.GetHit();
                    Instantiate(_skillEffect, hit.transform.position, Quaternion.identity);
                }
            }

            transform.position += targetDirection * _distance;
        }
    }
}
