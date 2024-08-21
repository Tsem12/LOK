using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.Music.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Music/Music FadeIn")]
    public class MMF_Music_FadeIn : MMF_Feedback
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [MMFInspectorGroup("Transition", true, 0)]
        [SerializeField] private float _duration = 1f;

        #pragma warning restore 0414
        #endregion
        
        //TODO: Override FeedbackDuration Property (using _duration)
        public override float FeedbackDuration => _duration;

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            MusicsGlobals.VolumeFader.FadeIn(_duration);
            //Call FadeIn from MusicVolumeFader (using _duration)
        }
    }
}