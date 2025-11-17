using UnityEngine;

public class DashAttackEffect : MonoBehaviour
{
    private void OnAnimationFinished()
    {
        gameObject.SetActive(false);
    }
}
