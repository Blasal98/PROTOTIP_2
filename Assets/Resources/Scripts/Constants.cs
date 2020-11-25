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

        public static float timeXTurn = 30;

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

        public static Color hoverFicha = new Color(1,0,0,1);
        public static Color normalFicha = new Color(1,1,1,1);

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
    public class Player
    {
        public static int starting_money = 500;
        public static int starting_moneyXTurn = 500;
        public static int starting_health = 100;
        public static int starting_attack = 1;
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

            public static float scaleX = 0.5385f/*(float)42 / 78*/;
            public static float scaleY = 0.5f;
        }

        public class Troop
        {
            public static int hPix = 90;
            public static int wPix = 90;
            public static float scale = 0.4f;

            public static Color nothingColor = Color.white;
            public static Color cammoColor = Color.yellow;
            public static Color armorColor = Color.gray;
            public static Color bothColor = Color.magenta;
            public static Color cam_armColor = Color.green;

            public static float property_cost = 0.5f;

            public class Soldier
            {
                public static int cost = 50;
                public static int health = 1;
            }
            public class Car
            {
                public static int cost = 200;
                public static int health = 4;
            }
            public class Tank
            {
                public static int cost = 800;
                public static int health = 10;
            }
            public class Plane
            {
                public static int cost = 1600;
                public static int health = 15;
            }
        }
        public class Building
        {
            
            public static int up_hPix = 90;
            public static int up_wPix = 90;
            public static float up_scale = 0.35f;
            public static float property_cost = 2f;

            public static Color selectedColor = new Color(1,0.5f,0,1);

            public class Trinchera
            {
                public static int cost = 250;
                public static int dmg = 1;
            }
            public class Sniper
            {
                public static int cost = 500;
                public static int dmg = 2;
            }
            public class AntiTank
            {
                public static int cost = 1000;
                public static int dmg = 5;
            }
            public class AntiAir
            {
                public static int cost = 2000;
                public static int dmg = 8;
            }
            public class Animation
            {
                public static float duration = 1;
                public static int steps = 25;
                public static float offset = 0.5f;
            }
           
        }
        public class City
        {
            public static int FCars_Cost = 500;
            public static int FCars_Plus = 50;

            public static int FTanks_Cost = 1000;
            public static int FTanks_Plus = 150;

            public static int FPlanes_Cost = 2000;
            public static int FPlanes_Plus = 300;

            public static int Milloras = 750;
            public static int MCammo = 1;
            public static int MArmor = 2;
            public static int MAir = 4;

        }
    }
    public class Layers
    {
        public static int zBackGround = 10;
        public static int zFicha = 2;
        public static int zFichaIndicator = 0;
        public static int zPath = 1;
        public static int zBuilding = -1;
        public static int zBuildingIndicator = -2;
        public static int zResult = -3;
    }
}

