using UnityEngine;

namespace Interaction
{
    public class LockAndKey : MonoBehaviour
    {
        [SerializeField] private Animator _lock;

        private void OnTriggerEnter(Collider other) => RunAnimation(other, true);
        private void OnTriggerExit(Collider other) => RunAnimation(other, false);
    
        private void RunAnimation(Collider other, bool forward)
        {
            if (!other.CompareTag($"Player")) return;
            _lock.SetBool("Forward", forward);
        }
    }
}