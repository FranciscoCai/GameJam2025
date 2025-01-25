using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private float stayTime = 15f;
    [SerializeField] private Transform SpawnPoint;

    private bool _isSpawned;
    private float _elapsedTime;

    [Header("SpawnEnemy")]
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Obstacle;
    public void SpawnLevel()
    {
        if (_levels == null) return;
        int randomIndex = Random.Range(0, _levels.Length);
        GameObject level = Instantiate(_levels[randomIndex], transform.position + (SpawnPoint.transform.position - transform.position)*2, Quaternion.identity) as GameObject;
        LevelData levelData = level.GetComponent<LevelData>();
        Transform[] enemySpawnPoints = levelData.EnemySpawnPoints;
        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            if (RandomNumber(66))
            {
                Instantiate(Enemy, enemySpawnPoints[i].transform.position, Quaternion.Euler(0, 180, 0));
            }
        }
        Transform[] obstacleSpawnPoints = levelData.ObstacleSpawnPoints;
        for (int i = 0; i < obstacleSpawnPoints.Length; i++)
        {
            if (RandomNumber(66))
            {
                Instantiate(Obstacle, obstacleSpawnPoints[i].transform.position, Quaternion.Euler(0, 180, 0));
            }
        }
        _isSpawned = true;
    }
    private bool RandomNumber(int probabilidad)
    {
        return Random.Range(0, 100) < probabilidad ? true : false;
    }
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
