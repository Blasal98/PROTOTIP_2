using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public class General
    {
        public static float imgDfltPiXUnit = 100;

        public static float CameraStart_x = 0;
        public static float CameraStart_y = 0f;

        public static Vector3 cursorCorrector = new Vector3(25, -25, 0);
        public static float cursorBorderError = 0.1f;

        public static int scrnDfltHeight = 1080;
        public static int scrnDfltWidth = 1920;

        public static float CameraRatio = 16 / 9;

        public static float scrnDfltHeightU = 5.4f * 2;
        public static float scrnDfltWidthU = 5.4f * 2 * 16 / 9;

        public class PhotoShop
        {
            public static int h_enemy = 350;
            public static int h_border = 30;
            public static int h_player = 700;
        }
    }
    public class Map
    {
        public static int w = 16;
        public static int h = 7;
        public static int path_size = 15;

        public class Local
        {
            public static float path_width = 0.25f;
            public static Color path_color = new Color(1, 0, 0, 1);
            public static int w_separation = 78;
        }
        public class Others
        {
            public static float path_width = 0.125f;
            public static Color path_color = new Color(1, 0, 0, 1);
            public static int w_separation = 42;
        }

    }
    public class Entity
    {
        public static float imgDfltPiXUnit = 100;
        public class Ficha
        {
            public static float hU = 1;
            public static float wU = 1.06f;

            public static int hPix = 100;
            public static int wPix = 106;
        }
        public class Ficha_Pequeña
        {
            public static float hU = 0.5f;
            public static float wU = 0.58f;

            public static int hPix = 50;
            public static int wPix = 58;
        }

    }
}

