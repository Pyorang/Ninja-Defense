using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("적 이동속도")]
    [Space]
    [SerializeField] private float _speed = 1f;

    [Header("적 사거리")]
    [Space]
    [SerializeField] private float _range = 4f;

    [Header("기타")]
    [Space] 
    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private GameObject _target;

    private bool _startAttack = false;
    private Vector3 _direction;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    
    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _direction = _target.transform.position.x - transform.position.x > 0 ? Vector3.right : Vector3.left;
        _spriteRenderer.flipX = _direction.x > 0 ? false : true;
        _animator.SetBool("Run", true);
    }

    private void OnEnable()
    {
        if(_target != null)
        {
            _direction = _target.transform.position.x - transform.position.x > 0 ? Vector3.right : Vector3.left;
            _spriteRenderer.flipX = _direction.x > 0 ? false : true;
            _animator.SetBool("Run", true);
        }
    }

    private void Update()
    {
        if(!_startAttack)
        {
            if (Vector3.Distance(_target.transform.position, transform.position) > _range)
            {
                transform.position += _direction * _speed * Time.deltaTime;
                return;
            }

            StartAttack();
        }
    }

    private void OnDisable()
    {
        _startAttack = false;
    }

    private void StartAttack()
    {
        _startAttack = true;
        _animator.SetBool("Run", false);
        _enemyAttack.StartAttack();
    }

    public void LockTarget(GameObject target)
    {
        _target = target;
    }
}
