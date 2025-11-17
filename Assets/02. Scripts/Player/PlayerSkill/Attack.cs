using UnityEngine;

public class Attack : Skill
{
    [Header("데미지 설정")]
    [Space]
    [SerializeField] private int _damage = 70;

    [Header("평타 사거리")]
    [SerializeField] private float _distance = 3f;

    public override void Execute()
    {
        _animator.SetTrigger("Attack");

        Vector3 targetDirection = gameObject.GetComponent<SpriteRenderer>().flipX ? Vector3.left : Vector3.right;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.right, _distance);
        // NOTE : 적 공격 처리
    }
}
