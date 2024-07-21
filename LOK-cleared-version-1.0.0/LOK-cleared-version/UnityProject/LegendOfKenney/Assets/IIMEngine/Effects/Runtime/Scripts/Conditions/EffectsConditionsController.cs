using UnityEngine;

namespace IIMEngine.Effects
{
    public class EffectsConditionsController : MonoBehaviour
    {
        #region DO NOT MODIFY
        
        [Header("Conditions")]
        [SerializeField] private AEffectCondition[] _conditions;

        private AEffect[] _effects;
        
        #endregion

        private void Awake()
        {
            _effects = GetComponents<AEffect>();
            foreach (AEffectCondition cond in _conditions)
            {
                cond.ConditionInit();
            }
            //Find All effects attached to this gameObject
            //Call ConditionInit() method for all conditions stored
        }

        private bool AreAllConditionValid()
        {
            foreach (AEffectCondition cond in _conditions)
            {
                if (!cond.IsValid())
                {
                    return false;
                }
            }
            return true;
        }
        
        private void Update()
        {
            bool areConditionsValids = AreAllConditionValid();
            
            foreach (AEffect effect in _effects)
            {
                if (areConditionsValids)
                {
                    effect.Play();
                }
                else
                {
                    effect.Stop();
                }
            }
            
            //TODO: call Play() method in attached playing effects if ALL conditions are valid
            //TODO: call Stop() method in attached non playing effects if conditions are not valid
        }
    }
}