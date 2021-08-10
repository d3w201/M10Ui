using System;
using UnityEngine;

namespace Entity.Cameras
{
    public class SpawnCamera : MonoBehaviour
    {
        public static event Action<GameObject> Triggered;
        private void OnTriggerEnter(Collider other)
        {
            if (!"Chiusky".Equals(other.gameObject.tag)) return;
            Triggered?.Invoke(this.gameObject);
        }
    }
}