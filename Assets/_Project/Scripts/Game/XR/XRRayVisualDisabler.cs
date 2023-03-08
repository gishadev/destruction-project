using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Gisha.Destruction.Game.XR
{
    [RequireComponent(typeof(XRBaseInteractor), typeof(XRInteractorLineVisual))]
    public class XRRayVisualDisabler : MonoBehaviour
    {
        [SerializeField] private GameObject reticle;
        [SerializeField] private ActionBasedController controller;

        private XRInteractorLineVisual _lineVisual;
        private XRTeleportationRay _interactor;

        private void Awake()
        {
            _interactor = GetComponent<XRTeleportationRay>();
            _lineVisual = GetComponent<XRInteractorLineVisual>();
            
            controller.selectAction.action.started += OnSelectActionEntered;
            controller.selectAction.action.canceled += OnSelectActionExited;
        }

        private void Start()
        {
            Disable();
        }

        private void OnDisable()
        {
            controller.selectAction.action.started -= OnSelectActionEntered;
            controller.selectAction.action.canceled -= OnSelectActionExited;
        }

        private void OnSelectActionEntered(InputAction.CallbackContext obj)
        {
            if (_interactor.IsActive)
                Enable();
        }

        private void OnSelectActionExited(InputAction.CallbackContext obj)
        {
            Disable();
        }

        public void Disable()
        {
            _lineVisual.enabled = false;
            reticle.SetActive(false);
        }

        public void Enable()
        {
            _lineVisual.enabled = true;
            reticle.SetActive(true);
        }
    }
}