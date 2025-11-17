using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("플레이어 이동")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private int _maxJumpCount = 2;

    private int _jumpCount = 0;
    private int JumpCount
    {
        get
        {
            return _jumpCount;
        }
        set
        {
            _jumpCount = value;
            _animator.SetInteger("JumpCount", value);
        }
    }

    private readonly int _groundLayer = 6;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRender;
    private Vector3 _direction;

    // NOTE : 입력 키 관련 변수
    private KeyCode _leftMoveKey = KeyCode.LeftArrow;
    private KeyCode _rightMoveKey = KeyCode.RightArrow;
    private KeyCode _downMoveKey = KeyCode.DownArrow;
    private KeyCode _jumpKey = KeyCode.Space;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HorizontalMove();
        GetJumpInput();
    }

    private void HorizontalMove()
    {
        _direction = Vector3.zero;

        if(Input.GetKey(_leftMoveKey))
        {
            _direction += Vector3.left;
        }

        if(Input.GetKey(_rightMoveKey))
        {
            _direction += Vector3.right;
        }

        transform.position += _direction * _moveSpeed * Time.deltaTime;
        FlipSprite(_direction);
        ProcessRunAnimation(_direction);
    }

    private void FlipSprite(Vector3 direction)
    {
        if(direction.x < 0)
        {
            _spriteRender.flipX = true;
        }

        if(direction.x > 0)
        {
            _spriteRender.flipX = false;
        }
    }

    private void ProcessRunAnimation(Vector3 direction)
    {
        string runParameter = "Run";

        if(Mathf.Abs(direction.x) > 0)
        {
            _animator.SetBool(runParameter, true);
        }
        else
        {
            _animator.SetBool(runParameter, false);
        }
    }

    private void GetJumpInput()
    {
        if(Input.GetKeyDown(_jumpKey) && _jumpCount >0)
        {
            JumpCount--;
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody.linearVelocityY = _jumpForce;
        _animator.SetTrigger("Jump");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == _groundLayer)
        {
            JumpCount = _maxJumpCount;
        }
    }
}
