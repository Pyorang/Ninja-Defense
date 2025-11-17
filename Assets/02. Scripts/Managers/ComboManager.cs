using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    private static ComboManager s_Instance;
    public static ComboManager Instance => s_Instance;

    [Header("콤보당 점수")]
    [Space]
    [SerializeField] private int _comboScore = 10;

    private bool _isContinuousAttack = false;
    private int _currentCombo = 0;
    public int CurrentCombo
    {
        get { return _currentCombo; }
        set
        {
            _currentCombo = value;
            _isContinuousAttack = _currentCombo > 0 ? true : false;
            _resetTimeElapsed = 0f;

            _comboText.text = $"{_currentCombo}";
            UpdateComboText();
        }
    }

    private static readonly int _comboNeedToCast = 3;
    private int _skillCombo = 0;

    [Header("콤보 초기화 시간")]
    [SerializeField] private float _comboResetTime = 3f;
    private float _resetTimeElapsed = 0f;

    [Header("콤보 텍스트")]
    [SerializeField] private Text _comboText;

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _comboText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(_isContinuousAttack)
        {
            _resetTimeElapsed += Time.deltaTime;

            if (_resetTimeElapsed >= _comboResetTime)
            {
                _comboText.gameObject.SetActive(false);
                CurrentCombo = 0;
                _skillCombo = 0;
                _resetTimeElapsed = 0f;
            }
        }
    }

    public void AddCombo(int combo)
    {
        for(int i = 0; i < combo; i++)
        {
            CurrentCombo++;
            ScoreManager.Instance.AddScore(_comboScore * CurrentCombo);
        }

        _skillCombo = Mathf.Min(_skillCombo + combo, _comboNeedToCast);
    }

    public bool UseSkill()
    {
        if(_skillCombo == _comboNeedToCast)
        {
            _skillCombo--;
            return true;
        }

        return false;
    }

    public void UpdateComboText()
    {
        if(_currentCombo > 1)
        {
            _comboText.gameObject.SetActive(true);
            float startScale = 2f;
            Vector3 targetScale = Vector3.one;
            float duration = 0.4f;

            _comboText.rectTransform.DOKill(true);
            _comboText.rectTransform.localScale = Vector3.one * startScale;
            _comboText.rectTransform.DOScale(targetScale, duration).SetEase(Ease.OutBack);
        }
    }
}
