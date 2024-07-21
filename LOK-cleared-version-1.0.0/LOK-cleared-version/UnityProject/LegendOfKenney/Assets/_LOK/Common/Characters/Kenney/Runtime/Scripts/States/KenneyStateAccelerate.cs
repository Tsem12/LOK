using IIMEngine.Movements2D;
using UnityEngine;

namespace LOK.Common.Characters.Kenney
{
    public class KenneyStateAccelerate : AKenneyState
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
        protected override void OnStateInit()
        {
            _lockedReader = StateMachine.GetComponent<IMove2DLockedReader>();
            _movReader = StateMachine.GetComponent<IMove2DDirReader>();
            _speedMaxReader = StateMachine.GetComponent<IMove2DSpeedMaxReader>();
            _orientWriter = StateMachine.GetComponent<IMove2DOrientWriter>();
            _speedWriter = StateMachine.GetComponent<IMove2DSpeedWriter>();
            //Find Movable Interfaces inside StateMachine
            //You will need to :
            // - Check if movements are locked
            // - Read MoveDir
            // - Read Move SpeedMax
            // - Write Move Orient
            // - Write Move Speed
        }

        protected override void OnStateEnter(AKenneyState previousState)
        {
            _orientWriter.OrientDir = _movReader.MoveDir;
            float speed = _speedWriter.MoveSpeed / _speedMaxReader.MoveSpeedMax;
            _timer = MovementsData.StartAccelerationDuration * speed;
            //Force OrientDir to MoveDir
            //Calculate _timer according to MoveSpeed / MoveSpeedMax / MovementsData.StartAccelerationDuration
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

            if (Vector2.Angle(_movReader.MoveDir, _orientWriter.OrientDir) > MovementsData.TurnBackAngleThreshold)
            {
                if(MovementsData.TurnBackDecelerationDuration > 0)
                    StateMachine.ChangeState(StateMachine.StateTurnBackDecelerate);
                
                if(MovementsData.TurnBackAccelerationDuration > 0)
                    StateMachine.ChangeState(StateMachine.StateTurnBackAccelerate);
                
                return;
            }

            _timer += Time.deltaTime;

            if (_timer > MovementsData.StartAccelerationDuration)
            {
                StateMachine.ChangeState(StateMachine.StateWalk);
                return;
            }

            float percentage = _timer / MovementsData.StartAccelerationDuration;
            _speedWriter.MoveSpeed = _speedMaxReader.MoveSpeedMax * percentage;

            _orientWriter.OrientDir = _movReader.MoveDir;

            //Go to State Idle if Movements are locked

            //If there is no MoveDir
            //Go to StateDecelerate if MovementsData.StopDecelerationDuration > 0
            //Go to StateIdle otherwise

            //If the angle between MoveDir and OrientDir > MovementsData.TurnBackAngleThreshold
            //If MovementsData.TurnBackDecelerationDuration > 0 => Go to StateTurnBackDecelerate
            //Else If MovementsData.TurnBackAccelerationDuration > 0 => Go to StateTurnBackAccelerate

            //Increment _timer with deltaTime

            //If _timer > MovementsData.StartAccelerationDuration
            //Go to StateWalk (acceleration is finished)

            //Calculate percent using timer and MovementsData.StartAccelerationDuration
            //Calculate MoveSpeed according to percent and MoveSpeedMax
            //Force OrientDir to MoveDir
        }
    }
}