using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    private static InGameManager s_Instance;
    public static InGameManager Instance => s_Instance;

    [Header("게임 시작 문구")]
    [Space]
    [SerializeField] private Text _gameStartText;
    [SerializeField] private float _displayTime = 2f;

    [Header("게임 배경")]
    [Space]
    [SerializeField] private SpriteRenderer[] _backgrounds;
    [SerializeField] private float _backGroundColorCycle = 60f;
    private float _timeElapsed = 0f;
    private float _cycleRatio, _colorValue, _halfCycle;

    [Header("게임 종료 판넬")]
    [Space]
    [SerializeField] private GameObject _gameOverPanel;

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
    }

    private void Start()
    {
        AudioManager.Instance.PlaySound("InGame", AudioType.BGM);
        StartCoroutine(StartDescription());
    }

    private void Update()
    {
        // 1. 시간 업데이트
        _timeElapsed += Time.deltaTime;
        _cycleRatio = Mathf.Repeat(_timeElapsed, _backGroundColorCycle) / _backGroundColorCycle;

        _halfCycle = _backGroundColorCycle / 2f;
        _colorValue = Mathf.PingPong(_timeElapsed, _halfCycle) / _halfCycle;

        Color newColor = new Color(1 - _colorValue, 1 - _colorValue, 1 - _colorValue);

        foreach (SpriteRenderer sr in _backgrounds)
        {
            if (sr != null)
            {
                sr.color = newColor;
            }
        }
    }

    public void OnClickRestartButton()
    {
        SceneLoader.Instance.ReloadScene();
    }

    public void OnClickExitButton()
    {
        SceneLoader.Instance.LoadScene(ESceneType.Lobby);
    }

    public void SetActivePanel()
    {
        _gameOverPanel.SetActive(true);
    }

    private IEnumerator StartDescription()
    {
        yield return new WaitForSeconds(_displayTime);
        _gameStartText.text = "Defend It!!";
        yield return new WaitForSeconds(_displayTime);
        _gameStartText.gameObject.SetActive(false);
    }
}
