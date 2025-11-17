using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    [Header("프리팹")]
    [Space]
    [SerializeField] protected GameObject _objectPrefab;

    [Header("풀링")]
    [SerializeField] protected int _objectPoolSize = 30;
    private GameObject[] _objectPool;

    private static ObjectFactory s_Instance;
    public static ObjectFactory Instance => s_Instance;

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

        PoolInit();
    }

    private void PoolInit()
    {
        _objectPool = new GameObject[_objectPoolSize];

        for (int i = 0; i < _objectPoolSize; i++)
        {
            GameObject spawnObject = Instantiate(_objectPrefab, transform);

            _objectPool[i] = spawnObject;
            spawnObject.SetActive(false);
        }
    }

    public GameObject GetObject(Vector3 position)
    {
        for (int i = 0; i < _objectPoolSize; i++)
        {
            GameObject spawnObject = _objectPool[i];

            if (spawnObject.activeSelf == false)
            {
                spawnObject.transform.position = position;
                spawnObject.SetActive(true);

                return spawnObject;
            }
        }

        return IncreasePoolSize(position);
    }

    private GameObject IncreasePoolSize(Vector3 position)
    {
        int newPoolSize = (int)(_objectPoolSize * 3 / 2);
        GameObject[] newPool = new GameObject[newPoolSize];

        for (int i = 0; i < _objectPoolSize; i++)
        {
            newPool[i] = _objectPool[i];
        }

        for (int i = _objectPoolSize; i < newPoolSize; i++)
        {
            newPool[i] = Instantiate(_objectPool[0], transform);
            newPool[i].SetActive(false);
        }

        _objectPool = newPool;
        _objectPoolSize = newPoolSize;

        _objectPool[_objectPoolSize - 1].SetActive(true);
        _objectPool[_objectPoolSize - 1].transform.position = position;
        return _objectPool[_objectPoolSize - 1];
    }
}
