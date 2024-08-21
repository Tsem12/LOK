using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IIMEngine.Camera
{
    public static class CameraPOIs
    {
        #region DO NOT MODIFY
        #pragma warning disable 0414
        
        private static List<CameraPOI> _activePOIs = new List<CameraPOI>();

        public static ReadOnlyCollection<CameraPOI> ActivePOIs { get; private set; } = new ReadOnlyCollection<CameraPOI>(_activePOIs);

        private static List<CameraPOIDetector> _activeDetectors = new List<CameraPOIDetector>();
        public static ReadOnlyCollection<CameraPOIDetector> ActiveDetectors { get; private set; } = new ReadOnlyCollection<CameraPOIDetector>(_activeDetectors);

        #pragma warning restore 0414
        #endregion

        public static void RegisterDetector(CameraPOIDetector detector)
        {
            //Add Detector into _activeDetectors List
            _activeDetectors.Add(detector);
        }

        public static void UnregisterDetector(CameraPOIDetector detector)
        {
            //Remove Detector into _activeDetectors List
            _activeDetectors.Remove(detector);
        }
        
        public static void RegisterPOI(CameraPOI cameraPOI)
        {
            //Add POIs into _activePOIs List
            _activePOIs.Add(cameraPOI);
        }

        public static void UnregisterPOI(CameraPOI cameraPOI)
        {
            //Remove POIs into _activePOIs List
            _activePOIs.Remove(cameraPOI);
        }
    }
}