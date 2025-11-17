using UnityEngine;

public class ShurikenAttack : Skill
{
    [Header("표창 프리팹")]
    [Space]
    [SerializeField] private GameObject _shurikenPrefab;

    [Header("표창 던지기 위치 조정")]
    [SerializeField] private float _offset = 1f;
    public override void Execute()
    {
        _animator.SetTrigger("Skill2");

        Vector3 targetDirection = gameObject.GetComponent<SpriteRenderer>().flipX ? Vector3.left : Vector3.right;
        Vector3 firePosition = transform.position + _offset * targetDirection;

        GameObject spawnedShuriken = Instantiate(_shurikenPrefab, firePosition, Quaternion.identity);
        Shuriken shuriken = spawnedShuriken.GetComponent<Shuriken>();
        shuriken.SetDirection(targetDirection);
    }
}
