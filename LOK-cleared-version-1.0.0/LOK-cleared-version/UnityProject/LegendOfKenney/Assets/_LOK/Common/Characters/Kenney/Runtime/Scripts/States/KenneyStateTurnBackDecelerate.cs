using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateTurnBackDecelerate : AKenneyState
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414

        private float _timer = 0f;

        #pragma warning restore 0414
        #endregion
        
        private IMove2DLockedReader _lockedReader;
        private IMove2DDirReader _movReader;
        private IMove2DSpeedMaxReader _speedMaxReader;
        private IMove2DOrientWriter _orientWriter;
        private IMove2DSpeedWriter _speedWriter;
        private IMove2DTurnBackWriter _turnBackWriter;
        protected override void OnStateInit()
        {
            _lockedReader = StateMachine.GetComponent<IMove2DLockedReader>();
            _movReader = StateMachine.GetComponent<IMove2DDirReader>();
            _speedMaxReader = StateMachine.GetComponent<IMove2DSpeedMaxReader>();
            _orientWriter = StateMachine.GetComponent<IMove2DOrientWriter>();
            _speedWriter = StateMachine.GetComponent<IMove2DSpeedWriter>();
            _turnBackWriter = StateMachine.GetComponent<IMove2DTurnBackWriter>();

            //Find Movable Interfaces inside StateMachine
            //You will need to :
            // - Check if movements are locked
            // - Read Move Dir
            // - Read Move SpeedMax
            // - Read Move Orient
            // - Write Move Speed
            // - Write Move IsTurningBack
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            Debug.Log(("TBACKDecelerate"));
            _turnBackWriter.IsTurningBack = true;
            
            float speed = _speedWriter.MoveSpeed / _speedMaxReader.MoveSpeedMax;
            _timer = MovementsData.TurnBackDecelerationDuration * (1 - speed);
            //Set IsTurningBack to true

            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.TurnBackDecelerationDuration
        }

        protected override void OnStateExit(AKenneyState nextState)
        {
            _turnBackWriter.IsTurningBack = false;
            //Set IsTurningBack to false
        }
        
        protected override void OnStateUpdate()
        {
            if (_lockedReader.AreMovementsLocked)
            {
                StateMachine.ChangeState(StateMachine.StateIdle);
            }
            //Go to State Idle if Movements are locked

            // if (_movReader.MoveDir != Vector2.zero && Vector2.Angle(_movReader.MoveDir, _orientWriter.OrientDir) > MovementsData.TurnBackAngleThreshold)
            // {
            //     StateMachine.ChangeState(StateMachine.StateAccelerate);
            //     return;
            // }
            
            _timer += Time.deltaTime;
            //Debug.Log(_timer);
            if (_timer > MovementsData.TurnBackDecelerationDuration)
            {
                StateMachine.ChangeState(_movReader.MoveDir != Vector2.zero ? StateMachine.StateTurnBackAccelerate : StateMachine.StateIdle);
                return;
            }

            float percent = _timer / MovementsData.TurnBackDecelerationDuration;
            _speedWriter.MoveSpeed = _speedMaxReader.MoveSpeedMax * (1-percent);
            //If there is MoveDir and the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
            //Go to StateAccelerate
            //Increment _timer with deltaTime

            //If _timer > MovementsData.TurnBackDecelerationDuration
            //Go to StateTurnBackAccelerate if there is MoveDir
            //Go to StateIdle otherwise

            //Calculate percent using timer and MovementsData.TurnBackDecelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
        }
    }
}