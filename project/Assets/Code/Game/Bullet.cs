using UnityEngine;

namespace MAG.Game
{
	public class Bullet : MonoBehaviour
	{
		private float damage;
		private Rigidbody cachedRigidbody;
        private Transform cachedTransform;

        private void Awake()
		{
			cachedRigidbody = GetComponent<Rigidbody>();
		}

		public void Setup(float speed, float damage)
		{
			cachedRigidbody.velocity = transform.forward * speed;
            cachedTransform = transform;
            this.damage = damage;
		}

		public void GiveDamage(Enemy enemy)
		{
			enemy.TakeDamage(damage);
            gameObject.SetActive(false);
		}

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Destoyer"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}