using System;
using Gisha.Destruction.Infrastructure.Misc;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Gisha.Destruction.Game.Player
{
    public class HandAnimationController : MonoBehaviour
    {
        [SerializeField] private ActionBasedController controller;
        [SerializeField] private float animationSpeed = 0.5f;
        
        private float _gripTarget, _triggerTarget;
        private float _gripCurrent, _triggerCurrent;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            GetInputs();
            AnimateHand();
        }

        private void GetInputs()
        {
            _gripTarget = controller.selectAction.action.ReadValue<float>();
            _triggerTarget = controller.activateAction.action.ReadValue<float>();
        }

        private void AnimateHand()
        {
            if (Math.Abs(_gripCurrent - _gripTarget) > 0.01f)
            {
                _gripCurrent = Mathf.MoveTowards(_gripCurrent, _gripTarget, Time.deltaTime * animationSpeed);
                _animator.SetFloat(Constants.GRIP_PARAM, _gripCurrent);
            }

            if (Math.Abs(_triggerCurrent - _triggerTarget) > 0.01f)
            {
                _triggerCurrent = Mathf.MoveTowards(_triggerCurrent, _triggerTarget, Time.deltaTime * animationSpeed);
                _animator.SetFloat(Constants.TRIGGER_PARAM, _triggerCurrent);
            }
        }
    }
}