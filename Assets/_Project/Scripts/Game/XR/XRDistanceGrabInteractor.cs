using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Gisha.Destruction.Game.XR
{
    public class XRDistanceGrabInteractor : XRBaseControllerInteractor
    {
        [SerializeField] private GameObject reticle;
        [Header("Raycast")] [SerializeField] private Transform fwdVector;
        [SerializeField] private float raycastRadius = 0.35f;
        [SerializeField] private float raycastDst = 5f;


        private List<IXRInteractable> _validTargets = new List<IXRInteractable>();
        private XRBaseInteractable _selectedObject;

        private new void Start()
        {
            // Deal with the cursor
            reticle.SetActive(false);
        }

        public override void ProcessInteractor(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractor(updatePhase);
            GetValidTargets(_validTargets);
        }

        public override void GetValidTargets(List<IXRInteractable> targets)
        {
            _selectedObject = null;
            targets.Clear();
            
            var ray = new Ray(fwdVector.position, fwdVector.forward);

            if (Physics.SphereCast(ray, raycastRadius, out var hitInfo, raycastDst))
            {
                if (hitInfo.collider != null && hitInfo.collider.TryGetComponent(out XRBaseInteractable interactable))
                {
                    if ((interactable.interactionLayers & interactionLayers) != 0 && !interactable.isSelected)
                    {
                        _selectedObject = interactable;
                        targets.Add(_selectedObject);
                    }
                }
            }

            if (_selectedObject != null && !isSelectActive)
                MoveCursor(_selectedObject.transform);
            else
                HideCursor();
        }

        // Tell the XRInteractionManager that we have an object that we can select for when the grab input is activated.
        public override bool CanSelect(XRBaseInteractable interactable)
        {
            bool selectActivated = _selectedObject == interactable || base.CanSelect(interactable);
            return selectActivated && (selectTarget == null || selectTarget == interactable);
        }

        private void MoveCursor(Transform target)
        {
            reticle.SetActive(true);
            reticle.transform.position = target.position;
        }

        private void HideCursor()
        {
            reticle.SetActive(false);
        }
    }
}