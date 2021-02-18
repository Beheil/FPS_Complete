using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private UnityEvent _event;
        public void RunInteract() => _event.Invoke();
    }
}