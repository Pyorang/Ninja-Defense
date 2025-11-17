using System.Collections;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    [SerializeField] private EnemyMove _enemyMove;
    [SerializeField] private EnemyAttack _enemyAttack;

    [Header("레이어 처리")]
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
        ComboManager.Instance.AddCombo(1);

        gameObject.layer = _diedEnemyLayer;

        _enemyMove.enabled = false;
        _enemyAttack.enabled = false;

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;
        StartCoroutine(DestroyAfterTime(animationLength));
    }

    private IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
