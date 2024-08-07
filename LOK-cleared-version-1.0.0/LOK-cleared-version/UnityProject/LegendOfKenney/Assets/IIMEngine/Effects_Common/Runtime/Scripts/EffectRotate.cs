﻿using System.Collections;
using IIMEngine.Effects;
using UnityEngine;

namespace IIMEngine.Effects.Common
{
    public class EffectRotate : AEffect
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Object to Rotate")]
        [SerializeField] private Transform _objectToRotate;

        public Transform ObjectToRotate => _objectToRotate;

        [Header("Modifiers")]
        [SerializeField] private AEffectModifierFloat _timeModifier;

        [Header("Rotation")]
        [SerializeField] private float _rotationAngle = 10f;
        [SerializeField] private float _rotationPeriod = 0.5f;
        [SerializeField] private float _rotationStopDelay = 0.5f;
        [SerializeField] private AnimationCurve _rotationCurve = AnimationCurve.Constant(0f, 1f, 0f);

        public float RotationAngle => _rotationAngle;
        
        [Header("Looping")]
        [SerializeField] private bool _isLooping = true;

        private Vector3 _eulerAnglesDelta = Vector3.zero;
        private float _timer = 0f;

        #pragma warning restore 0414
        #endregion
        
        protected override void OnEffectReset()
        {
            _timer = 0;
            _objectToRotate.localEulerAngles -= _eulerAnglesDelta;
            _eulerAnglesDelta = Vector3.zero;
            //Reset Timer
            //Remove rotation delta from objectToRotate localRotation (using eulerAngles)
            //Reset rotation delta Z
        }

        protected override IEnumerator OnEffectEndCoroutine()
        {
            while (_timer < _rotationPeriod)
            {
                ObjectToRotate.localEulerAngles -= _eulerAnglesDelta;
                _timer += Time.deltaTime;
                float percentage = _timer / _rotationPeriod;
                percentage = _rotationCurve.Evaluate(percentage);
                _eulerAnglesDelta = new Vector3(0, 0 ,_rotationAngle * percentage);
                ObjectToRotate.localEulerAngles += _eulerAnglesDelta;
                yield return null;
            }
            
            //TODO: Do not interrupt rotation
            //Wait for rotation stop delay (while loop)
                //Remove rotation delta from objectToRotate localRotation (using eulerAngles)
                //Increment Timer with delta time
                //Calculating percentage between timer and rotationPeriod
                //Applying animation curve on percentage
                //Set eulerAngles delta Z according to percentage and rotationAngle
                //Add rotation delta from objectToRotate localRotation (using eulerAngles)
                //Wait for next frame (with yield instruction)
                
            
            yield break;
        }
        
        protected override void OnEffectEnd()
        {
            _timer = 0;
            _objectToRotate.localEulerAngles -= _eulerAnglesDelta;
            _eulerAnglesDelta = Vector3.zero;
            //Reset Timer
            //Remove rotation delta from objectToRotate localRotation (using eulerAngles)
            //Reset rotation delta Z
        }

        protected override void OnEffectUpdate()
        {
            _objectToRotate.localEulerAngles -= _eulerAnglesDelta;
            _timer += Time.deltaTime;
            if (_isLooping)
            {
                if(_timer > _rotationPeriod) {_timer = 0;}
            }

            float percentage = _timer / _rotationPeriod;
            percentage = _rotationCurve.Evaluate(percentage);
            _eulerAnglesDelta = new Vector3(0, 0, percentage * _rotationAngle);
            _objectToRotate.localEulerAngles += _eulerAnglesDelta;
            
            //Remove rotation delta from objectToRotate localRotation (using eulerAngles)
            //Increment timer with delta time (bonus : Applying factor to deltaTime using timeModifier)
            //If effect is looping, timer must loop between [0, rotationPeriod]
            //Calculating percentage between timer and rotationPeriod
            //Set eulerAngles Z according to percentage and rotationAngle
            //Add rotation delta from objectToRotate localRotation (using eulerAngles)
        }
    }
}