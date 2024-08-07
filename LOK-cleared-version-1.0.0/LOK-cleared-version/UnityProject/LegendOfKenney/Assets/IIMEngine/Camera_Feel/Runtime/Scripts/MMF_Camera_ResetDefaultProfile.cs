﻿using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.Camera.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Camera/Camera Reset Default Profile")]
    public class MMF_Camera_ResetDefaultProfile : MMF_Feedback
    {
        [MMFInspectorGroup("Transition", true)]
        [SerializeField] private CameraProfileTransition _transition;
        public override float FeedbackDuration
        {
            get
            {
                if (_transition != null)
                {
                    return _transition.Duration;
                }
                return 0;
            }

        }
        //TODO: Override FeedbackDuration Property (using _transition)
        //Don't forget to check if transition is null (can be null when adding effect at the first time in the inspector)

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            CameraGlobals.Profiles.ResetToDefaultProfile(_transition);
        }
    }
}