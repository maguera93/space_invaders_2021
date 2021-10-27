using System;
using MAG.Utils;
using UnityEngine;

namespace MAG.Model
{
	public class PlayerModel : IModel
	{
		public event Action<PlayerModel> die = delegate { };
		public event Action<PlayerModel> killedEnemy = delegate { };
		public event Action<PlayerModel, float> damageTaken = delegate { };
        public event Action<PlayerModel> moveRight = delegate { };
        public event Action<PlayerModel> moveLeft = delegate { };
        public event Action<PlayerModel> shoot = delegate { };
		
		public float Speed { get; }
		public float BulletDamage { get; }
		public float BulletSpeed { get;}
		public float BulletCooldown { get; }
        public float PowerUpDuration { get; }
		public int Score { get; private set; }
		public int HitPoints { get; private set; }
		
		public PlayerModel(PlayerConfig config)
		{
			Speed = config.Speed;
			HitPoints = config.Hitpoints;
			BulletDamage = config.BulletDamage;
			BulletCooldown = config.BulletCooldown;
			BulletSpeed = config.BulletSpeed;
            PowerUpDuration = config.PowerUpDuration;
		}
		
		public bool IsDead()
		{
			return HitPoints == 0;
		}

		public void TakeDamage(float damage)
		{
			if (IsDead())
			{
				return;
			}
			
			HitPoints = (int) Mathf.Max(0, HitPoints - damage);
			damageTaken(this, damage);
			
			if (!IsDead())
			{
				return;
			}
            
			die(this);
		}

		public void KillEnemy()
		{
			Score += 1;
			killedEnemy(this);
		}

        public void MoveRight()
        {
            moveRight(this);
        }

        public void MoveLeft()
        {
            moveLeft(this);
        }

        public void Shoot()
        {
            shoot(this);
        }
        
	}
}