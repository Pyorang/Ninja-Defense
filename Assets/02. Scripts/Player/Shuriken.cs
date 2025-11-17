using Unity.VisualScripting;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [Header("수리검 이동속도")]
    [SerializeField] private float _speed = 0.05f;
    private Vector3 _direction;

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        this._direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyStat enemy = collision.gameObject.GetComponent<EnemyStat>();

        if (enemy != null)
        {
            enemy.GetHit();
            Destroy(gameObject);
        }
    }
}
