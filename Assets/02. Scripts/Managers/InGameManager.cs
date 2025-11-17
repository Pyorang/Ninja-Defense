using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private static InGameManager s_Instance;
    public static InGameManager Instance => s_Instance;

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
}
