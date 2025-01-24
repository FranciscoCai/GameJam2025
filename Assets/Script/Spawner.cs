using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private float stayTime = 15f;

    private bool _isSpawned;
    private float _elapsedTime;

    public void SpawnLevel()
    {
        int randomIndex = Random.Range(0, _levels.Length);
        GameObject level = Instantiate(_levels[randomIndex], transform.position,Quaternion.identity) as GameObject;
        _isSpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isSpawned) return;
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime > stayTime)
        {
            _elapsedTime = 0;
            _isSpawned = false;
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
