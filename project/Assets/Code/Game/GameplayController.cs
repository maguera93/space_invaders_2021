using Cysharp.Threading.Tasks;
using MAG.Model;
using MAG.Popups;
using MAG.Utils;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEngine.SceneManagement;
#endif

namespace MAG.Game
{
    public class GameplayController : MonoBehaviour
    {
        private const int ENEMY_POOL_LENGHT = 20;
        private const int POWERUP_POOL_LENGHT = 5;
        private const int ENEMY_SNAKE_PROBABILITY = 40;

        [Header("Config")]
        [SerializeField] private EnemyConfig enemyConfig;
        [SerializeField] private EnemyConfig enemySnakeConfig;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private SpawnerConfig spawnerConfig;
        [SerializeField] private SpawnerConfig spawnerSnakeConfig;
        [Header("Prefabs")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject powerUpPrefab;
        [Header("Spawn Points")]
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private Transform enemySpawnPoint;
        [Header("Pool Container")]
        [SerializeField] private Transform enemyContainer;
        [SerializeField] private Transform bulletContainer;
        [SerializeField] private Transform powerUpContainer;

        private ObjectPool enemyPool;
        private ObjectPool enemySnakePool;
        private ObjectPool powerUpPool;

        private Player player;
        private float lastTimeSpawnedEnemy;

        private void Start()
        {
            SpawnPlayer();

#if UNITY_EDITOR
            if (Game.Instance == null)
            {
                SceneManager.LoadScene(0);
                return;
            }
#endif

            var topBar = Game.Get<PopupManager>().Get<TopBar>();
            topBar.Setup(player.Model);
            Game.Get<PopupManager>().Open<TopBar>().Forget();

            var inputs = Game.Get<PopupManager>().Get<InputsPopup>();
            inputs.Setup(player.Model);
            Game.Get<PopupManager>().Open<InputsPopup>().Forget();

            enemyPool = new ObjectPool(spawnerConfig.Prefab, ENEMY_POOL_LENGHT, enemyContainer);
            enemySnakePool = new ObjectPool(spawnerSnakeConfig.Prefab, ENEMY_POOL_LENGHT, enemyContainer);
            powerUpPool = new ObjectPool(powerUpPrefab, POWERUP_POOL_LENGHT, powerUpContainer);
        }

        private void Update()
        {
            if (player.Model.IsDead())
            {
                return;
            }

            if (lastTimeSpawnedEnemy + spawnerConfig.SpawnDelay <= Time.timeSinceLevelLoad)
            {
                lastTimeSpawnedEnemy = Time.timeSinceLevelLoad;
                SpawnEnemy();
            }
        }

        #region Spawn
        private void SpawnPlayer()
        {
            var playerGo = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity, transform);
            player = playerGo.GetComponent<Player>();
            player.Setup(new PlayerModel(playerConfig), bulletPrefab, bulletContainer);

            player.onDie += HandlePlayerDeath;
        }
        
        private void SpawnEnemy()
        {
            int rand = Random.Range(0, 100);

            if (rand < 100 - ENEMY_SNAKE_PROBABILITY)
            {
                EnemySetup(enemyPool, enemyConfig, spawnerConfig);
            }
            else
            {
                EnemySetup(enemySnakePool, enemySnakeConfig, spawnerSnakeConfig);
            }
        }

        private void EnemySetup(ObjectPool pool, EnemyConfig config, SpawnerConfig spawnerConfig)
        {
            Vector3 pos = enemySpawnPoint.transform.position +
                       new Vector3(
                           Random.Range(spawnerConfig.SpawnPosition.x, spawnerConfig.SpawnPosition.y),
                           0, 0);

            var g = pool.SpawnObject(pos);
            var enemy = g.GetComponent<Enemy>();
            enemy.Setup(new EnemyModel(config));
            enemy.onImpact += HandleEnemyImpact;
            enemy.onDie += HandleEnemyDeath;
        }

        private void SpawnPowerUp(Enemy enemy)
        {
            int rand = Random.Range(0, 100);

            if (rand < enemy.Model.PowerUpProbability)
            {
                var g = powerUpPool.SpawnObject(enemy.transform.position);
                var p = g.GetComponent<PowerUp>();
                p.Setup(playerConfig.PowerUpSpeed);
            }
        }
        #endregion

        #region Handles
        private void HandleEnemyDeath(Enemy obj)
        {
            obj.onImpact -= HandleEnemyImpact;
            obj.onDie -= HandleEnemyDeath;
            SpawnPowerUp(obj);
            obj.gameObject.SetActive(false);
        }

        private void HandleEnemyImpact(Enemy enemy, GameObject other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var playerComponent))
            {
                playerComponent.TakeDamage(enemy.Model.Damage);
            }
            else if (other.gameObject.TryGetComponent<Bullet>(out var bullet))
            {
                bullet.GiveDamage(enemy);

                if (enemy.Model.IsDead())
                {
                    player.Model.KillEnemy();
                }
            }
            else if (other.CompareTag("Destoyer"))
            {
                enemy.gameObject.SetActive(false);
            }
        }

        private void HandlePlayerDeath(Player playerModel)
        {
            var endPopup = Game.Get<PopupManager>().Get<EndPopup>();
            endPopup.Setup(player.Model);
            Game.Get<PopupManager>().Open<EndPopup>().Forget();
        }
        #endregion
    }
}
