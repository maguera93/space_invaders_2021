using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MAG.Game
{
    public class PowerUp : MonoBehaviour
    {
        private Rigidbody cachedRigidbody;
        private Transform cachedTransform;

        private void Awake()
        {
            cachedRigidbody = GetComponent<Rigidbody>();
            cachedTransform = transform;
        }

        public void Setup(float speed)
        {
            cachedRigidbody.velocity = cachedTransform.forward * -speed;
        }
    }
}
