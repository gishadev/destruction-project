using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Gisha.Destruction.Game.XR
{
    public class XRTeleportationRay : XRRayInteractor
    {
        [SerializeField] private XRBaseInteractor[] leftGrabInteractors;

        private bool _isActive;

        public bool IsActive => _isActive;

        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            CheckActive();
            if (_isActive)
                return;

            base.OnHoverEntered(args);
        }

        private void Update()
        {
            CheckActive();
        }

        private void CheckActive()
        {
            foreach (var interactor in leftGrabInteractors)
            {
                _isActive = interactor.interactablesSelected.Count == 0 &&
                            interactor.interactablesHovered.Count == 0 && !interactor.hasSelection &&
                            !interactor.hasHover;
                allowSelect = _isActive;

                if (!_isActive)
                    break;
            }
        }
    }
}