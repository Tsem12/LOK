using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.Camera.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Camera/Camera Effect Shake")]
    public class MMF_Camera_EffectShake : MMF_Feedback
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [MMFInspectorGroup("Shake", true)]
        [SerializeField] private float _shakeDuration = 1.0f;
        [SerializeField] private float _shakePower = 0.1f;
        [SerializeField] private float _shakePeriod = 0.05f;

        private CameraEffect _cameraEffect = new CameraEffect();

        #pragma warning restore 0414
        #endregion
        
        //TODO: Override FeedbackDuration Property (using _shakeDuration)
        public override float FeedbackDuration => _shakeDuration;

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            Owner.StartCoroutine(_CoroutineShake());
        }

        private IEnumerator _CoroutineShake()
        {
            //Add _cameraEffect into CameraEffects
            //Implements shake effects using _shakeDuration / _shakePeriod / _shakePower
            CameraGlobals.Effects.AddEffect(_cameraEffect);
            float timer = 0;
            while (timer < _shakeDuration)
            {
                timer += Time.deltaTime;
                float percentage = Mathf.PingPong(timer / _shakePeriod, 1);
                _cameraEffect.PositionDelta = new Vector3(_shakePower * percentage,_shakePower * percentage,0);
                yield return null;
            }
        }
    }
}