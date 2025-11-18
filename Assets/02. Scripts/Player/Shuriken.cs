using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [Header("수리검 이동속도")]
    [SerializeField] private float _speed = 0.05f;

    [Header("수리검 사라지는 시간")]
    [SerializeField] private float _destroyTime = 5.0f;

    private bool _hitTheWall = false;
    private Vector3 _direction;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private int _groundLayer = 6;
    [SerializeField] private Sprite _hitWallSprite;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!_hitTheWall)
        {
            transform.position += _direction * _speed * Time.deltaTime;
        }
    }

    public void SetDirection(Vector3 direction)
    {
        this._direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyStat enemy = collision.gameObject.GetComponent<EnemyStat>();

        if (!_hitTheWall && enemy != null)
        {
            enemy.GetHit();
            AudioManager.Instance.PlaySound("ShurikenHit", AudioType.SFX);
            gameObject.SetActive(false);
        }

        if (collision.gameObject.layer == _groundLayer)
        {
            _hitTheWall = true;
            _animator.SetTrigger("HitWall");
            AudioManager.Instance.PlaySound("ShurikenHit", AudioType.SFX);

            if (_direction.x < 0)
            {
                _spriteRenderer.flipX = true;
            }

            StartCoroutine(DestroyObject());
        }
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(_destroyTime);
        gameObject.SetActive(false);
    }
}
