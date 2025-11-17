using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    [Header("키보드 키 설정")]
    [Space]
    [SerializeField] protected KeyCode _pressButton;

    [Header("재사용 대기 시간")]
    [Space]
    protected static bool s_isAttacking = false;
    [SerializeField] private float _waitTimeToControl = 1f;

    [Header("재사용 대기 시간 이미지")]
    [Space]
    [SerializeField] protected Image _coolTimeImage;
    protected static List<Image> s_images = new List<Image>();

    protected PlayerMove _playerMove;
    protected Animator _animator;

    private void Awake()
    {
        s_images.Add(_coolTimeImage);
        _playerMove = GetComponent<PlayerMove>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(_pressButton) && !s_isAttacking)
        {
            s_isAttacking = true;

            StartCoroutine(UpdateCoolTimeImage());

            _playerMove.SetMovementLock(true);
            Execute();
            StartCoroutine(WaitAttackTime());
        }
    }

    protected IEnumerator WaitAttackTime()
    {
        yield return new WaitForSeconds(_waitTimeToControl);
        s_isAttacking = false;
        _playerMove.SetMovementLock(false);
    }

    protected IEnumerator UpdateCoolTimeImage()
    {
        float startTime = Time.time;
        float endTime = startTime + _waitTimeToControl;
        float coolDownDuration = _waitTimeToControl;

        foreach(Image image in s_images)
        {
            image.fillAmount = 1;
        }

        while (Time.time < endTime)
        {
            float elapsedTime = Time.time - startTime;
            float progress = elapsedTime / coolDownDuration;
            foreach (Image image in s_images)
            {
                image.fillAmount = 1f - progress;
            }
            yield return null;
        }

        foreach (Image image in s_images)
        {
            image.fillAmount = 0;
        }
    }

    public abstract void Execute();
}
