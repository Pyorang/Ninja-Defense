using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager s_Instance;
    public static ScoreManager Instance => s_Instance;

    [Header("점수 텍스트")]
    [Space]
    [SerializeField] private Text _currentScoreTextUI;
    [SerializeField] private Text _bestScoreTextUI;

    private int _currentScore = 0;
    public int CurrentScore
    {
        get { return _currentScore; }
        set
        {
            _currentScore = value;
            AccentScoreUI(_currentScoreTextUI);

            if (_currentScore > _bestScore)
            {
                _bestScore = _currentScore;
                AccentScoreUI( _bestScoreTextUI);
            }

            Refresh();
        }
    }
    private int _bestScore = 0;

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Refresh()
    {
        _currentScoreTextUI.text = $"Current Score : {_currentScore:N0}";
        _bestScoreTextUI.text = $"Best Score : {_bestScore:N0}";
    }

    public void AddScore(int score)
    {
        if (score <= 0) return;

        CurrentScore += score;
    }

    private void AccentScoreUI(Text text)
    {
        float startScale = 2f;
        Vector3 targetScale = Vector3.one;
        float duration = 0.4f;

        text.rectTransform.DOKill(true);
        text.rectTransform.localScale = Vector3.one * startScale;
        text.rectTransform.DOScale(targetScale, duration).SetEase(Ease.OutBack);
    }

    public void Save()
    {
        UserData data = new UserData
        {
            BestScore = _bestScore
        };

        string json = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString("UserData", json);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey("UserData"))
        {
            string json = PlayerPrefs.GetString("UserData");
            UserData data = JsonUtility.FromJson<UserData>(json);

            if (data != null)
            {
                _bestScore = data.BestScore;
            }
            else
            {
                _bestScore = 0;
            }
        }

        else
        {
            _bestScore = 0;
        }

        Refresh();
    }
}
