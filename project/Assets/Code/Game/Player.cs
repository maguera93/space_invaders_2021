using System;
using MAG.Model;
using MAG.Utils;
using UnityEngine;

namespace MAG.Game
{
	public class Player : MonoBehaviour
	{
        private const int BULLET_POOL_LENGTH = 30;
        private const float TRIPLESHOOT_ANGLE = 30;
        private const float MOVEMENT_LIMIT = 5.5F;

        public event Action<Player> onDie = delegate { };

        private PlayerModel model;
		private float lastTimeShot;
        private float powerUpTime;
        private bool powerUp = false;
        private Transform cachedTransform;
        
        private ObjectPool bulletPool;

		public PlayerModel Model => model;

		public void Setup(PlayerModel model, GameObject bulletPrefab, Transform bulletContainer)
		{
			this.model = model;
			model.die += OnModelDie;
            model.moveRight += MoveRight;
            model.moveLeft += MoveLeft;
            model.shoot += Shoot;

            powerUpTime = Time.timeSinceLevelLoad;

            bulletPool = new ObjectPool(bulletPrefab, BULLET_POOL_LENGTH, bulletContainer);

            cachedTransform = transform;
		}

		private void OnModelDie(PlayerModel obj)
		{
			onDie(this);
            model.moveRight -= MoveRight;
            model.moveLeft -= MoveLeft;
            model.shoot -= Shoot;
        }

		public void TakeDamage(float damage)
		{
			model.TakeDamage(damage);
		}


        private void Update()
		{
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				MoveLeft(model);
			}
			else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				MoveRight(model);
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				Shoot(model);
			}
#endif
            if (powerUp)
                CheckPowerUpFinished();
        }


        #region Movement
        private void MoveLeft(PlayerModel model)
		{
            if (cachedTransform.position.x <= -MOVEMENT_LIMIT)
                return;

            var pos = cachedTransform.position;
			pos.x -= model.Speed * Time.deltaTime;
            cachedTransform.position = pos;
		}

		private void MoveRight(PlayerModel model)
		{
            if (cachedTransform.position.x >= MOVEMENT_LIMIT)
                return;

			var pos = cachedTransform.position;
			pos.x += model.Speed * Time.deltaTime;
            cachedTransform.position = pos;
		}

		private void Shoot(PlayerModel model)
		{
			if (lastTimeShot + model.BulletCooldown <= Time.timeSinceLevelLoad)
			{
                if (!powerUp)
                    SimpleShoot(Quaternion.identity);
                else
                    TripleShoot();
			}
		}
        #endregion

        #region ShootLogic
        private void SimpleShoot(Quaternion rotation)
        {
            var bulletGo = bulletPool.SpawnObject(cachedTransform.position, rotation);
            var bullet = bulletGo.GetComponent<Bullet>();
            bullet.Setup(model.BulletSpeed, model.BulletDamage);
            lastTimeShot = Time.timeSinceLevelLoad;
        }

        private void TripleShoot()
        {
            Vector3 rotation = Vector3.zero;
            rotation.y = -TRIPLESHOOT_ANGLE;

            for (int i = 0; i < 3; ++i)
            {
                SimpleShoot(Quaternion.Euler(rotation));
                rotation.y += TRIPLESHOOT_ANGLE;
            }
        }
        #endregion

        #region PowerUp
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PowerUp"))
            {
                other.gameObject.SetActive(false);
                OnPowerUp();
            }
        }

        private void OnPowerUp()
        {
            powerUp = true;
            powerUpTime = Time.timeSinceLevelLoad;
        }

        private void CheckPowerUpFinished()
        {
            if (powerUpTime + model.PowerUpDuration <= Time.timeSinceLevelLoad)
                powerUp = false;
        }
        #endregion
    }
}