using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    private static LobbyManager s_Instance;
    public static LobbyManager Instance => s_Instance;

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
        //오디오 처리
    }

    public void OnClickStartButton()
    {
        SceneLoader.Instance.LoadScene(ESceneType.InGame);
    }

    public void OnClickEndButton()
    {
        Application.Quit();
    }
}
