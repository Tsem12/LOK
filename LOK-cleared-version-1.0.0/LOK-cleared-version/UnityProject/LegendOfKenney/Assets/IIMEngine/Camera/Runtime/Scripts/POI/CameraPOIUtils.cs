using System.Linq;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

namespace IIMEngine.Camera
{
    public static class CameraPOIUtils
    {
        public static int FindPOIsNearby(CameraPOI[] resultsFound)
        {
            //Find Active POIs inside ActiveDetectors and fill resultsFound array
            //It's a range test between POIs and POIs Detector
            //Both have a Range Property
            //Be careful : do not fill more that resultsFound.Length
            int result = 0;
            for (int i = 0; i < CameraPOIs.ActiveDetectors.Count; i++)
            {
                for (int j = 0; j < resultsFound.Length; j++)
                {
                    resultsFound[j] = null;
                    
                    if(j >= CameraPOIs.ActivePOIs.Count)
                        continue;

                    if (Vector3.Distance(CameraPOIs.ActiveDetectors[i].Position, CameraPOIs.ActivePOIs[j].Position) <
                        CameraPOIs.ActiveDetectors[i].Range + CameraPOIs.ActivePOIs[j].Range)
                    {
                        if (!resultsFound.Contains(CameraPOIs.ActivePOIs[j]))
                        {
                            resultsFound[j] = CameraPOIs.ActivePOIs[j];
                            result++;
                        }
                    }
                }
            }
            return result;
        }
    }
}