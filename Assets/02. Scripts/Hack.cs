using UnityEngine;
using UnityEngine.UI;

public class Hack : MonoBehaviour
{
    [Header("체력 관련")]
    [Space]
    [SerializeField] private float _hp = 100f;
    public float HP
    {
        get { return _hp; }
        set
        {
            _hp = Mathf.Max(0, value);
            _slider.value = _hp / 100f;
            if (_hp == 0)
            {
                ProcessGameOver();
            }
        }
    }

    [Header("체력 슬라이드바")]
    [Space]
    [SerializeField] private Slider _slider;

    public void GetDamage(float damage)
    {
        HP -= damage;
    }

    public void ProcessGameOver()
    {
        ScoreManager.Instance.Save();
        InGameManager.Instance.SetActivePanel();
    }
}
