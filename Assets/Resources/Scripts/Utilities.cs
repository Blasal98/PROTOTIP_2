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
        public static Color lerpColor(Color a, Color b, float f)
        {
            return new Color(lerp(a.r, b.r, f), lerp(a.g, b.g, f), lerp(a.b, b.b, f), lerp(a.a, b.a, f));
        }

    }
    public class Pair_IntInt
    {
        public int i1;
        public int i2;

        Pair_IntInt() { }
        public Pair_IntInt(int _i1, int _i2)
        {
            i1 = _i1; i2 = _i2;
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
    public class Pair_TroopInt
    {
        public Troop t;
        public int i;

        Pair_TroopInt() { }
        public Pair_TroopInt(Troop _t, int _i)
        {
            t = _t; i = _i;
        }

    }
}