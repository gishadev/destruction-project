using System;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Gisha.Destruction.Game.XR
{
    [RequireComponent(typeof(XROrigin), typeof(CharacterControllerDriver), typeof(CharacterController))]
    public class XRCharacterMovementHelper : MonoBehaviour
    {
        private XROrigin _xrOrigin;
        private CharacterController _characterController;
        private CharacterControllerDriver _driver;

        private void Awake()
        {
            _driver = GetComponent<CharacterControllerDriver>();
            _characterController = GetComponent<CharacterController>();
            _xrOrigin = GetComponent<XROrigin>();
        }

        private void Update()
        {
            UpdateCharacterController();
        }

        private void UpdateCharacterController()
        {
            if (_xrOrigin == null || _characterController == null)
                return;

            var height = Mathf.Clamp(_xrOrigin.CameraInOriginSpaceHeight, _driver.minHeight, _driver.maxHeight);

            Vector3 center = _xrOrigin.CameraInOriginSpacePos;
            center.y = height / 2f + _characterController.skinWidth;

            _characterController.height = height;
            _characterController.center = center;
        }
    }
}