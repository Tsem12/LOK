using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateTurnBackAccelerate : AKenneyState
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
            // - Write Move Orient
            // - Write Move Speed
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            Debug.Log(("TBACKAcelerate"));
            float speed = _speedWriter.MoveSpeed / _speedMaxReader.MoveSpeedMax;
            _timer = MovementsData.TurnBackAccelerationDuration * speed;
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.TurnBackAccelerationDuration
        }

        protected override void OnStateUpdate()
        {
            
            if (_lockedReader.AreMovementsLocked)
            {
                StateMachine.ChangeState(StateMachine.StateIdle);
                return;
            }

            if (_movReader.MoveDir == Vector2.zero)
            {
                StateMachine.ChangeState(MovementsData.StopDecelerationDuration > 0 ? StateMachine.StateDecelerate : StateMachine.StateIdle);
                return;
            }

            // if (Vector2.Angle(_movReader.MoveDir, _orientWriter.OrientDir) > MovementsData.TurnBackAngleThreshold)
            // {
            //     StateMachine.ChangeState(StateMachine.StateTurnBackDecelerate);
            //     return;
            // }
            
            _timer += Time.deltaTime;
            //Debug.Log(_timer);
            if (_timer > MovementsData.TurnBackAccelerationDuration)
            {
                StateMachine.ChangeState(StateMachine.StateWalk);
                return;
            }

            float percent = _timer / MovementsData.TurnBackDecelerationDuration;
            _speedWriter.MoveSpeed = _speedMaxReader.MoveSpeedMax * percent;
            _orientWriter.OrientDir = _movReader.MoveDir;
            //Go to State Idle if Movements are locked

            //If there is no MoveDir
            //Go to StateDecelerate if MovementsData.StopDecelerationDuration > 0
            //Go to StateIdle otherwise

            //If the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
            //Go to StateTurnBackDecelerate

            //Increment _timer with deltaTime

            //If _timer > MovementsData.TurnBackAccelerationDuration
            //Go to StateWalk (acceleration is finished)

            //Calculate percent using timer and MovementsData.TurnBackAccelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
            //Force OrientDir to MoveDir
        }
    }
}