using MoreMountains.Feedbacks;
using UnityEngine;

namespace IIMEngine.Music.Feel
{
    [AddComponentMenu("")]
    [FeedbackPath("Music/Music Resume")]
    public class MMF_Music_Resume : MMF_Feedback
    {
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            MusicsGlobals.PlaylistManager.ResumeMusic();
            //Call ResumeMusic from MusicsPlaylistManager
        }
    }
}