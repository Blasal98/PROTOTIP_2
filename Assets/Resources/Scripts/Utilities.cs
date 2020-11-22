using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Maths
    {
        public static float mapping(float x, float in_min, float in_max, float out_min, float out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }
        public static float lerp(float a, float b, float f)
        {
            return a + f * (b - a);
        }
        public static Vector2 lerpVec2(Vector2 a,Vector2 b, float f)
        {
            return new Vector2(lerp(a.x,b.x,f),lerp(a.y,b.y,f));
        }

    }
    public class Pair_FichaInt
    {
        public Ficha f;
        public int i;

        Pair_FichaInt() { }
        public Pair_FichaInt(Ficha _f, int _i) {
            f = _f; i = _i;
        }

    }
}