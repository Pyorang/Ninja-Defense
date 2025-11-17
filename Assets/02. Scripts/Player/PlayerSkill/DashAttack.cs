using UnityEngine;

public class DashAttack : Skill
{
    [Header("데미지 설정")]
    [Space]
    [SerializeField] private int _damage = 10;

    [Header("스킬 사거리")]
    [SerializeField] private float _distance = 1f;

    public override void Execute()
    {
        _animator.SetTrigger("Skill1");

        Vector3 targetDirection = gameObject.GetComponent<SpriteRenderer>().flipX ? Vector3.left : Vector3.right;
        transform.position += targetDirection * _distance;

        // NOTE ; 적공격 처리 + 특별 애니메이션 처리
    }
}
