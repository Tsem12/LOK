using System;
using System.Collections;
using UnityEngine;

namespace IIMEngine.ScreenTransitions
{
    public class ScreenTransitionsManager : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        public static ScreenTransitionsManager Instance { get; private set; }
        
        [SerializeField] private bool _autoInit = true;
        
        private ScreenTransition[] _allTransitions;
        
        #pragma warning restore 0414
        #endregion

        private void Awake()
        {
            Instance = this;
            
            if (_autoInit) {
                Init();
            }
        }

        public void Init()
        {
            _allTransitions = _FindAllTransitions();
            _InitAllTransitions();
        }

        public ScreenTransition GetScreenTransition(string transitionID)
        {
            foreach (ScreenTransition trans in _allTransitions)
            {
                if (transitionID == trans.TransitionID)
                {
                    return trans;
                }
            }
            return null;
        }
        public IEnumerator PlayAndWaitTransition(string transitionID)
        {
            //Call PlayTransition and wait until transition is finished
            PlayTransition(transitionID);
            ScreenTransition t = GetScreenTransition(transitionID);
            yield return new WaitUntil(() => !t.IsPlaying);
        }

        public ScreenTransition PlayTransition(string transitionID)
        {
            ScreenTransition t = GetScreenTransition(transitionID);
            t?.Play();
            return t;
            //Find Transition using transitionID and return it
            //Play Transition if transition found
            //Return Transition
        }

        private ScreenTransition[] _FindAllTransitions()
        {
            //Find All ScreenTransitions
            return FindObjectsOfType<ScreenTransition>();
        }

        private void _InitAllTransitions()
        {
            foreach (ScreenTransition st in _allTransitions)
            {
                st.Init();
            }
            //Call ScreenTransition.Init() method for each found transitions inside _allTransitions
        }
    }
}