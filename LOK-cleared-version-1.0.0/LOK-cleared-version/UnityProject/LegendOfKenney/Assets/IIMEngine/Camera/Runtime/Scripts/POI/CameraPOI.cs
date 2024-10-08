﻿using UnityEngine;

namespace IIMEngine.Camera
{
    public class CameraPOI : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        [Header("Weight")]
        [SerializeField] private float _weight = 1f;
        public float Weight => _weight;
        
        [Header("Offset")]
        [SerializeField] private Vector3 _offset = Vector3.zero;
        public Vector3 Offset => _offset;
        

        [Header("Range")]
        [SerializeField] private float _range = 1f;
        public float Range => _range;

        public Vector3 Position => transform.position;

        public Vector3 PositionWithOffset => Position + Offset;
        
        #pragma warning restore 0414
        #endregion

        private void OnEnable()
        {
            //Register this object into static class CameraPOIs
            CameraPOIs.RegisterPOI(this);
        }

        private void OnDisable()
        {
            //Unregister this object into static class CameraPOIs
            CameraPOIs.UnregisterPOI(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(Position + Offset, _range);
        }
    }
}