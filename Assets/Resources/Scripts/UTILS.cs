using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTILS
{
    public class MATH
    {
        public static float Hexagon_X_Displacement(float radius)
        {
            return (radius * (3.0f / 2.0f));
        }
        public static float Hexagon_Z_Displacement(float radius)
        {
            return (radius / (2.0f * Mathf.Tan(Mathf.Deg2Rad * 30.0f)));
        }
    }
}
