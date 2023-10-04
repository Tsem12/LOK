using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateIdle : AKenneyState
    {
        private IMove2DSpeedWriter _speedWritter;
        private IMove2DLockedReader _lockedReader;
        private IMove2DDirReader _movDirReader;
        protected override void OnStateInit()
        {
            _speedWritter = StateMachine.GetComponent<IMove2DSpeedWriter>();
            _lockedReader = StateMachine.GetComponent<IMove2DLockedReader>();
            _movDirReader = StateMachine.GetComponent<IMove2DDirReader>();
            //Find Movable Interfaces inside StateMachine
            //You will need to write Speed and check if movements are locked and read MoveDir
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            Debug.Log("Enter idle");
            _speedWritter.MoveSpeed = 0;
            //Force MoveSpeed to 0
        }

        protected override void OnStateUpdate()
        {
            if(_lockedReader.AreMovementsLocked)
                return;

            if (_movDirReader.MoveDir != Vector2.zero)
            {
                StateMachine.ChangeState(MovementsData.StartAccelerationDuration > 0 ? StateMachine.StateAccelerate : StateMachine.StateWalk);
            }
            //Do nothing if movements are locked

            //If there is MoveDir
                //Change to StateAccelerate if MovementsData.StartAccelerationDuration > 0
                //Change to StateWalk otherwise
        }
    }
}