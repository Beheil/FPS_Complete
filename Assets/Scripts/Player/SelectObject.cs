using Interaction;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class SelectObject : MonoBehaviour
    {
        [SerializeField] private string selectableTag = "Selectable";
        [SerializeField] private Camera _camera;
        [SerializeField] private Material highlightMaterial;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private float rayRange;
        
        [SerializeField] private Transform _selection;

        private void OnFire()
        {
            if (_selection == null) return;
            _selection.GetComponent<Interactable>().RunInteract();
        }
        
        private void Update()
        {
            if (_selection != null)
            {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                selectionRenderer.material = defaultMaterial;
                _selection = null;
            }
            
            var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var hit) && hit.distance < rayRange )
            {
                var selection = hit.transform;

                if (selection.CompareTag(selectableTag))
                {
                    var selectionRenderer = selection.GetComponent<Renderer>();
                    defaultMaterial = selectionRenderer.material;
                    
                    if (selectionRenderer != null)
                    {
                        selectionRenderer.material = highlightMaterial;
                    }
                    _selection = selection;
                }
            }
        }
    }
}