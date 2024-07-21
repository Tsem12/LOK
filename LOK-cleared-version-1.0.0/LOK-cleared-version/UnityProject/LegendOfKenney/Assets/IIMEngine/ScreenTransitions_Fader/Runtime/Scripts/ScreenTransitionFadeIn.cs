using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IIMEngine.ScreenTransitions.Fader
{
    public class ScreenTransitionFadeIn : ScreenTransition
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [SerializeField] private Image _imageRenderer;
        [SerializeField] private float _fadeInDuration = 1f;
        [SerializeField] private AnimationCurve _fadeInCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        #pragma warning restore 0414
        #endregion
        
        protected override IEnumerator PlayLoop()
        {
            //TODO: Implements FadeIn
            //Get Current Alpha from ImageRenderer.
            //Lerp ImageRenderer Current Alpha to 1 using _fadeInDuration & _fadeInCurve.
            //Don't forget => you will need a loop (for, while). You are inside a Coroutine.
            float alpha = _imageRenderer.color.a;
            float timer = 0;
            while (timer < _fadeInDuration)
            {
                timer += Time.deltaTime;
                float newA = Mathf.Lerp(alpha, 1,_fadeInCurve.Evaluate(timer / _fadeInDuration));
                _imageRenderer.color = new Vector4(_imageRenderer.color.r, _imageRenderer.color.g,
                    _imageRenderer.color.b, newA);
                yield return null;
            }
        }
    }
}