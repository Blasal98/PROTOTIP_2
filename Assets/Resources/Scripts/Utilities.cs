﻿using System.Collections;
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
    }
}