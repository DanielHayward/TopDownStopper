using UnityEngine;

namespace DKH
{
    public class RigidbodyMover : MonoBehaviour, IMover
    {
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        public void Move(Vector3 velocity)
        {
            rb.AddForce(velocity * 10);
        }
    }
}