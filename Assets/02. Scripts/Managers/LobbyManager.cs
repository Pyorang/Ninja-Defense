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
        //AudioManager.Instance.PlaySound("Lobby", AudioType.BGM);
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
