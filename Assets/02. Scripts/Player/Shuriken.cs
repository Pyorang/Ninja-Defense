using UnityEngine;

public class Shuriken : MonoBehaviour
{
    [Header("표창 데미지")]
    [SerializeField] private int _damage = 50;

    [Header("수리검 이동속도")]
    [SerializeField] private float _speed = 0.05f;
    private Vector3 direction;

    private void Update()
    {
        transform.position += direction * _speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //적과 맞았을 떄 & 벽과 부딪혔을 떄
    }
}
