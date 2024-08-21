using UnityEngine;

namespace IIMEngine.Camera
{
    public class CameraBoundsManager : MonoBehaviour
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        private bool _boundsEnabled = false;
        private Rect _boundsRect;
        
        #pragma warning restore 0414
        #endregion

        private void Awake()
        {
            CameraGlobals.Bounds = this;
        }

        public void EnableBounds(Rect boundsRect)
        {
            _boundsEnabled = true;
            _boundsRect = boundsRect;
        }

        public void DisableBounds()
        {
            _boundsEnabled = false;
        }
        
        public void ManualUpdate(UnityEngine.Camera camera, Transform cameraTransform)
        {
            cameraTransform.position = ClampPosition(cameraTransform.position, camera);
        }

        public Vector3 ClampPosition(Vector3 position, UnityEngine.Camera camera)
        {
            //Check if bounds are enabled
            //Get BottomLeft Point using camera.ScreenToWorldPoint (using pos (0,0))
            //Get TopRight Point using camera.ScreenToWorldPoint / camera.pixelWidth / camera.pixelHeight
            //Calculate screenSize with TopRight and BottomLeft points
            //Clamp position according to bounds and screenSize
            if (!_boundsEnabled)
                return position;

            Vector3 botLeft = camera.ScreenToWorldPoint(Vector3.zero);
            Vector3 topRight = camera.ScreenToWorldPoint(new Vector2(camera.pixelWidth, camera.pixelHeight));

            float xCorrection = 0f;
            float yCorrection = 0f;

            if (botLeft.x < _boundsRect.xMin)
            {
                xCorrection = _boundsRect.xMin - botLeft.x;
            }
            else if (topRight.x > _boundsRect.xMax)
            {
                xCorrection = _boundsRect.xMax - topRight.x;
            }

            if (botLeft.y < _boundsRect.yMin)
            {
                yCorrection = _boundsRect.yMin - botLeft.y;
            }
            else if(topRight.y > _boundsRect.yMax)
            {
                yCorrection = _boundsRect.yMax - topRight.y;
            }

            position += new Vector3(xCorrection, yCorrection);
            
            return position;
        }
    }
}