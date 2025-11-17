using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager s_Instance;
    public static CameraManager Instance => s_Instance;

    [Header("카메라 설정")]
    [Space]
    [SerializeField] private Camera _leftHighLightCamera;
    [SerializeField] private Camera _rightHighLightCamera;

    [Header("카메라 하이라이트 시간")]
    [Space]
    [SerializeField] private float _highlightTIme = 1f;

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

    public void HighlightCharacter(bool isRight)
    {
        StartCoroutine(ProcessHighlight(isRight));
    }

    private IEnumerator ProcessHighlight(bool isRight)
    {
        Camera highLightingCam = isRight ? _rightHighLightCamera : _leftHighLightCamera;

        highLightingCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(_highlightTIme);
        highLightingCam.gameObject.SetActive(false);
    }
}
