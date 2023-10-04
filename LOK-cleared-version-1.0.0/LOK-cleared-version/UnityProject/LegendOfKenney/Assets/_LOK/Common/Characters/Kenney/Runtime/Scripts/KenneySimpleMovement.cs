            // - Read Move Dir
using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneySimpleMovement : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Movements")]
        [SerializeField] private KenneyMovementsData _movementsData;
        
        #pragma warning restore 0414
        #endregion

        private IMove2DDirReader _iMove2DDirReader;
        private IMove2DOrientWriter _iMove2DOrientWriter;
        private IMove2DSpeedWriter _iMove2DSpeedWriter;
        private IMove2DLockedReader _iMove2DLockedReader;
        private IMove2DSpeedMaxReader _iMove2DSpeedMaxReader;

        private void Awake()
        {
            _iMove2DDirReader = GetComponent<IMove2DDirReader>();
            _iMove2DOrientWriter = GetComponent<IMove2DOrientWriter>();
            _iMove2DSpeedWriter = GetComponent<IMove2DSpeedWriter>();
            _iMove2DSpeedMaxReader = GetComponent<IMove2DSpeedMaxReader>();
            _iMove2DLockedReader = GetComponent<IMove2DLockedReader>();

            // Find Movable Interfaces (at the root of this gameObject)
            // You will need to :
            //  - Check if movements are locked
            //  - Read Move Dir
            //  - Write Move Orient
            //  - Write Move Speed
        }

        private void Update()
        {
            if (_iMove2DLockedReader.AreMovementsLocked)
            {
                _iMove2DSpeedWriter.MoveSpeed = 0;
            }

            if (_iMove2DDirReader.MoveDir != Vector2.zero)
            {
                
                _iMove2DOrientWriter.OrientDir = _iMove2DDirReader.MoveDir;
                _iMove2DSpeedWriter.MoveSpeed = _iMove2DSpeedMaxReader.MoveSpeedMax;
            }
            else
            {
                _iMove2DSpeedWriter.MoveSpeed = 0;
            }
            //If Movements are locked
            //Force MoveSpeed to 0

            //If there is MoveDir
                //Set MoveSpeed to MovementsData.SpeedMax
                //Set Move OrientDir to Movedir
            //Else
                //Set MoveSpeed to 0
        }
    }
}