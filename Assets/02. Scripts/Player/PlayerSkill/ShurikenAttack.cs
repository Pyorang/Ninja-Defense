using UnityEngine;
using UnityEngine.UI;

public class ShurikenAttack : Skill
{
    [Header("표창 개수")]
    [Space]
    [SerializeField] private int _currentShurikenCount = 10;
    public int ShurikenCount
    {
        get { return _currentShurikenCount; }
        set
        {
            _currentShurikenCount = value;
            _currentShurikenCount = Mathf.Max(0, _currentShurikenCount);
            _shurikenCountText.text = $"{_currentShurikenCount}";
        }
    }
    [SerializeField] private Text _shurikenCountText;

    [Header("표창 프리팹")]
    [Space]
    [SerializeField] private GameObject _shurikenPrefab;

    [Header("표창 던지기 위치 조정")]
    [SerializeField] private float _offset = 1f;
    public override void Execute()
    {
        _animator.SetTrigger("Skill2");
        AudioManager.Instance.PlaySound("X", AudioType.SFX);

        if(ShurikenCount > 0)
        {
            Vector3 targetDirection = gameObject.GetComponent<SpriteRenderer>().flipX ? Vector3.left : Vector3.right;
            Vector3 firePosition = transform.position + _offset * targetDirection;

            GameObject spawnedShuriken = Instantiate(_shurikenPrefab, firePosition, Quaternion.identity);

            ShurikenCount--;

            Shuriken shuriken = spawnedShuriken.GetComponent<Shuriken>();
            shuriken.SetDirection(targetDirection);
        }
    }
}
