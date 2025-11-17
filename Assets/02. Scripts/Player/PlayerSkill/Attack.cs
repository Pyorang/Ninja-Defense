using UnityEngine;

public class Attack : Skill
{
    [Header("평타 사거리")]
    [SerializeField] private float _distance = 3f;

    public override void Execute()
    {
        _animator.SetTrigger("Attack");

        Vector3 targetDirection = gameObject.GetComponent<SpriteRenderer>().flipX ? Vector3.left : Vector3.right;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, targetDirection, _distance);
        foreach(RaycastHit2D hit in hits)
        {
            EnemyStat enemy = hit.transform.gameObject.GetComponent<EnemyStat>();

            if( enemy != null )
            {
                enemy.GetHit();
            }
        }
    }
}
