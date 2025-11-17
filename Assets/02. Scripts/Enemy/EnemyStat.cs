using System.Collections;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    [Header("적 아이템 스폰 확률")]
    [Space]
    [SerializeField] private int _spawnRate = 30;

    [Header("드롭하는 아이템")]
    [Space]
    [SerializeField] private GameObject _itemObject;

    [Header("기타 컴포넌트들")]
    [Space]
    [SerializeField] private EnemyMove _enemyMove;
    [SerializeField] private EnemyAttack _enemyAttack;

    [Header("레이어 처리")]
    [Space]
    [SerializeField] private int _enemyLayer = 7;
    [SerializeField] private int _diedEnemyLayer = 8;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        gameObject.layer = _enemyLayer;
    }

    public void GetHit()
    {
        _animator.SetTrigger("Die");
        AudioManager.Instance.PlaySound("EnemyHit", AudioType.SFX);
        DropItem();
        ComboManager.Instance.AddCombo(1);

        gameObject.layer = _diedEnemyLayer;

        _enemyMove.enabled = false;
        _enemyAttack.enabled = false;

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;
        StartCoroutine(DestroyAfterTime(animationLength));
    }

    private void DropItem()
    {
        int randomNum = Random.Range(1, 101);
        if(randomNum <= _spawnRate)
        {
            Instantiate(_itemObject, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
