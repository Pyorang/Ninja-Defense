using UnityEngine;

public class ShurikenItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShurikenAttack shurikenAttack = collision.GetComponent<ShurikenAttack>();

        if(shurikenAttack != null)
        {
            shurikenAttack.AddShuriken(1, transform.position);
            gameObject.SetActive(false);
        }
    }
}
