using UnityEngine;
using UnityEngine.UI;

public class ShurikenAttack : Skill
{
    [Header("표창 개수")]
    [Space]
    [SerializeField] private int _currentShurikenCount = 10;
    [SerializeField] private int _maxShurikenCount = 15;
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

    [Header("표창 먹는 효과")]
    [Space]
    [SerializeField] private GameObject _glowEffectPrefab;

    [Header("표창 던지기 위치 조정")]
    [SerializeField] private float _offset = 2f;
    public override void Execute()
    {
        _animator.SetTrigger("Skill2");
        AudioManager.Instance.PlaySound("X", AudioType.SFX);

        if(ShurikenCount > 0)
        {
            Vector3 targetDirection = gameObject.GetComponent<SpriteRenderer>().flipX ? Vector3.left : Vector3.right;
            Vector3 firePosition = transform.position + _offset * targetDirection;

            GameObject spawnedShuriken = ShurikenIFactory.Instance.GetObject(firePosition);

            ShurikenCount--;

            Shuriken shuriken = spawnedShuriken.GetComponent<Shuriken>();
            shuriken.SetDirection(targetDirection);
        }
    }

    public void AddShuriken(int addCount, Vector3 position)
    {
        ShurikenCount = Mathf.Min(_maxShurikenCount, ShurikenCount + addCount);
        AudioManager.Instance.PlaySound("Item", AudioType.SFX);
        Instantiate(_glowEffectPrefab, position, Quaternion.identity);
    }
}
