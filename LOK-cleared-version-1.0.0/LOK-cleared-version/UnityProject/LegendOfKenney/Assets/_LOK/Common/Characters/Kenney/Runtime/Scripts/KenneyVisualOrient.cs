using System.Collections;
using IIMEngine.Movements2D;
using UnityEngine;
using UnityEngine.UIElements;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyVisualOrient : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        [Header("Entity Root")]
        [SerializeField] private GameObject _entityRoot;

        [Header("Orient")]
        [SerializeField] private Transform _orientRoot;

        [Header("Flip Effect")]
        [SerializeField] private float _flipDuration ;

        private IMove2DOrientReader _orientReader;
        private KenneyStateMachine _stateMachine;

        private Coroutine _flipRoutine;
        private float _startScaleX = 0f;

        private bool _isFlipping = false;
        
        #pragma warning restore 0414
        #endregion

        private void Awake()
        {
            _orientReader = GetComponent<IMove2DOrientReader>();
            _stateMachine = GetComponent<KenneyStateMachine>();
            _startScaleX = _orientRoot.localScale.x;
            //Store _startScaleX using _orientRoot
        }

        private void OnEnable()
        {
            _orientRoot.localScale = new Vector3(_orientReader.OrientX,_orientRoot.localScale.y,_orientRoot.localScale.z);
            //Set orientRoot localScale using orientReader orientX
        }

        private void OnDisable()
        {
            _isFlipping = false;
            //Reset FLipping State
        }

        private void Update()
        {

            if (_orientReader.OrientDir.x > 0 && _orientRoot.localScale.x < 0)
            {
                if (_flipRoutine != null)
                {
                    StopCoroutine(_flipRoutine);
                    _flipRoutine = null;
                }
                _flipRoutine = StartCoroutine(FlipRoutine(_startScaleX));
                //_orientRoot.localScale = new Vector3(_startScaleX, _orientRoot.localScale.y, _orientRoot.localScale.z);
            }
            else if(_orientReader.OrientDir.x < 0 && _orientRoot.localScale.x > 0)
            {
                if (_flipRoutine != null)
                {
                    StopCoroutine(_flipRoutine);
                    _flipRoutine = null;
                }
                _flipRoutine = StartCoroutine(FlipRoutine(-_startScaleX));
                //_orientRoot.localScale = new Vector3(-_startScaleX, _orientRoot.localScale.y, _orientRoot.localScale.z);
            }
            //Detect if kenney need to flip ScaleX (using orientReader and current scale.x)
            //Bonus : you can create a flip animation using _flipDuration
        }

        IEnumerator FlipRoutine(float to)
        {
            float timer = 0;
            float baseOrient = _orientRoot.localScale.x;
            while (timer < _flipDuration)
            {
                timer += Time.deltaTime;
                float value = Mathf.Lerp(baseOrient, to, timer / _flipDuration);
                _orientRoot.localScale = new Vector3(value, _orientRoot.localScale.y, _orientRoot.localScale.z);
                yield return null;
            }
            _flipRoutine = null;
        }
    }
    
}