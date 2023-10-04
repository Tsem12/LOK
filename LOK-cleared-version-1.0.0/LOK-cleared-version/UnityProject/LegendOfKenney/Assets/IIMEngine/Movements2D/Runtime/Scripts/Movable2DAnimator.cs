using UnityEngine;

namespace IIMEngine.Movements2D
{
    public class Movable2DAnimator : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Movable")]
        [SerializeField] private GameObject _movableGameObject;

        [Header("Parameters")]
        [SerializeField] private string _isMovingParameter = "IsMoving";
        private int _isMovingParameterHash;
        
        [Header("Animation Speed")]
        [SerializeField] private float _animatorSpeedMin = 0.5f;
        [SerializeField] private float _animatorSpeedMax = 1f;
        
        #pragma warning restore 0414
        #endregion

        private IMove2DSpeedReader _speedReader;
        private IMove2DSpeedMaxReader _speedMaxReader;
        private IMove2DLockedReader _lockReader;
        private Animator _animator;
        private bool _isMoving;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _speedReader = _movableGameObject.GetComponent<IMove2DSpeedReader>();
            _speedMaxReader = _movableGameObject.GetComponent<IMove2DSpeedMaxReader>();
            _lockReader = _movableGameObject.GetComponent<IMove2DLockedReader>();
            
            //Convert _isMovingParameter to Hash 
            //(Improve performance when calling Animator.SetParameter)
            _isMovingParameterHash = Animator.StringToHash(_isMovingParameter);
            
            //Find Movable Interfaces inside _movableGameObject needed to check if object is moving
            //(You'll probably need to check if object movements are locked and if object move speed > 0)
            
            //Find Animator (attached to this gameObject)
        }

        private void Update()
        {
            _isMoving = _speedReader.MoveSpeed != 0 ? true : false;
            _animator.SetBool(_isMovingParameterHash, _isMoving);
            _animator.speed = Mathf.Clamp(_speedReader.MoveSpeed / _speedMaxReader.MoveSpeedMax, _animatorSpeedMin, _animatorSpeedMax);

            //Check if object is moving (store it inside a bool)
            //Bonus : Get Object movement speed and speed max to interpolate animator speed
            //Set animator parameter bool "IsMoving" according to movements infos
        }
    }
}