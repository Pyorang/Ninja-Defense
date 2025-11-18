using System.Collections;
using UnityEngine;

public class DashAttack : Skill
{
    [Header("스킬 사거리")]
    [Space]
    [SerializeField] private float _distance = 1f;

    [Header("스킬 이펙트")]
    [Space]
    [SerializeField] private GameObject _skillEffect;

    [Header("적 레이어 설정")]
    [Space]
    [SerializeField] private int _enemyLayer = 7;

    [Header("땅 레이어 설정")]
    [Space]
    [SerializeField] private int _groundLayer = 6;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_pressButton) && !s_isAttacking)
        {
            Execute();
        }
    }

    public override void Execute()
    {
        if(ComboManager.Instance.UseSkill())
        {
            bool hitWall = false;
            RaycastHit2D wall = new RaycastHit2D();

            s_isAttacking = true;
            _playerMove.SetMovementLock(true);
            StartCoroutine(WaitAttackTime());
            StartCoroutine(UpdateCoolTimeImage());

            _animator.SetTrigger("Skill1");

            Vector3 targetDirection = gameObject.GetComponent<SpriteRenderer>().flipX ? Vector3.left : Vector3.right;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, targetDirection, _distance);

            if(hits.Length > 3)
            {
                CameraManager.Instance.HighlightCharacter(targetDirection.x > 0);
                AudioManager.Instance.PlaySound("Amazing", AudioType.SFX);
            }

            else
            {
                StartCoroutine(MomentaryStop());
                AudioManager.Instance.PlaySound("TimeStop", AudioType.SFX);
            }

            if (hits.Length > 1)
            {
                AudioManager.Instance.PlaySound("LeftShift", AudioType.SFX);
            }
            else
            {
                AudioManager.Instance.PlaySound("Z", AudioType.SFX);
            }

            foreach (RaycastHit2D hit in hits)
            {
                EnemyStat enemy = hit.transform.gameObject.GetComponent<EnemyStat>();

                if (enemy != null && enemy.gameObject.layer == _enemyLayer)
                {
                    enemy.GetHit();
                    DashAttackEffectFactory.Instance.GetObject(hit.transform.position);
                }

                if(hit.collider.gameObject.layer == _groundLayer)
                {
                    hitWall = true;
                    wall = hit;
                }
            }

            if (hitWall)
            {
                transform.position = wall.point - (Vector2)(targetDirection * transform.localScale.x / 2f);
            }
            else
            {
                transform.position += targetDirection * _distance;
            }
        }
    }

    private IEnumerator MomentaryStop()
    {
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(_waitTimeToControl);
        Time.timeScale = originalTimeScale;
    }

    public void ActivateUI()
    {
        if (!s_images.Contains(_coolTimeImage))
        {
            s_images.Add(_coolTimeImage);
            _coolTimeImage.fillAmount = 0;
        }
    }

    public void DeactivateUI()
    {
        if (s_images.Contains(_coolTimeImage))
        {
            s_images.Remove(_coolTimeImage);
            _coolTimeImage.fillAmount = 1;
        }
    }
}
