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
    [SerializeField] private GameObject[] Enemy;
    [SerializeField] private GameObject[] Obstacle;
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
                int randomEnemy = Random.Range(0, Enemy.Length);
                GameObject toInstance = Instantiate(Enemy[randomEnemy], enemySpawnPoints[i].transform.position, Quaternion.Euler(0, 180, 0));
                toInstance.transform.SetParent(gameObject.transform);
            }
        }
        Transform[] obstacleSpawnPoints = levelData.ObstacleSpawnPoints;
        for (int i = 0; i < obstacleSpawnPoints.Length; i++)
        {
            if (RandomNumber(66))
            {
                int randomObstacle = Random.Range(0,Obstacle.Length);
                Quaternion rotation = Obstacle[randomObstacle].transform.rotation;
                GameObject toInstance = Instantiate(Obstacle[randomObstacle], obstacleSpawnPoints[i].transform.position, rotation);
                toInstance.transform.SetParent(gameObject.transform);
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
        if (!_isSpawned || GameManager.instance.modoAtaque) return;
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime > stayTime)
        {
            _elapsedTime = 0;
            _isSpawned = false;
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
