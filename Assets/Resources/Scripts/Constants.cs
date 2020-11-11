using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public class General
    {
        public static float imgDfltPiXUnit = 100;

        public static float TownHall_x = 0;
        public static float TownHall_y = -16;
        public static float ZTownHall_x = 0;
        public static float ZTownHall_y = 20;
        public static float CameraStart_x = 0;
        public static float CameraStart_y = -14f;

        public static Vector3 cursorCorrector = new Vector3(25, -25, 0);
        public static float cursorBorderError = 0.1f;

        public static float scrnDfltHeight = 1080;
        public static float scrnDfltWidth = 1920;


        public static float K_SEPARATION_FORCE = 0.9f;
        public static float K_COHESION_FORCE = 0.1f;


        public static float buildingsExtraView = 5;
        public static float buildingsExtraAtk = 5;


        public static int startMeatQ = 100;
        public static int startIronQ = 0;
        public static int startWoodQ = 100;
    }
    public class Entity
    {
        public static float hbBG_W = 120;
        public static float hbBG_H = 20;
        public static float hbFL_W = 100;
        public static float hbFL_H = 10;

        public static float dfltScaleX = 0.3f;
        public static float dfltScaleY = 0.3f;

        public static float imgDfltPiXUnit = 100;

        public class Building
        {

            public static float imgDfltHPix = 1000;
            public static float imgDfltWPix = 1000;

            public class TownHall
            {
                public static float health_max = 2000;
                public static float damageXSecond = 2;
                public static float attack_radius = 3;
                public static float view_radius = 6;

                public static int mC = 1000;
                public static int iC = 1000;
                public static int wC = 1000;
            }
            public class Farm
            {
                public static float health_max = 500;

                public static int mC = 50;
                public static int iC = 0;
                public static int wC = 100;
            }
            public class Foundry
            {
                public static float health_max = 500;

                public static int mC = 100;
                public static int iC = 0;
                public static int wC = 400;
            }

            public class Sawmill
            {
                public static float health_max = 500;

                public static int mC = 0;
                public static int iC = 0;
                public static int wC = 50;
            }
            public class Barracks
            {
                public static float health_max = 300;

                public static int mC = 50;
                public static int iC = 0;
                public static int wC = 100;
            }
            public class Barn
            {
                public static float health_max = 300;

                public static int mC = 500;
                public static int iC = 0;
                public static int wC = 300;
            }
            public class ZTownHall
            {
                public static float health_max = 2000;
                public static float damageXSecond = 2;
                public static float attack_radius = 3;
                public static float view_radius = 6;

            }


        }
        public class Character
        {
            public static float imgDfltHPix = 800;
            public static float imgDfltWPix = 500;

            public class Troop1
            {
                public static float health_max = 100;
                public static float damageXSecond = 15;
                public static float attack_radius = 3;
                public static float view_radius = 6;
                public static float velocity_max = 2;
                public static float force_max = 1000;

                public static int mC = 10;
                public static int iC = 0;
                public static int wC = 20;
            }
            public class Troop2
            {
                public static float health_max = 200;
                public static float damageXSecond = 30;
                public static float attack_radius = 3;
                public static float view_radius = 6;
                public static float velocity_max = 1.25f;
                public static float force_max = 1000;

                public static int mC = 20;
                public static int iC = 20;
                public static int wC = 50;
            }
            public class Villager
            {
                public static float health_max = 20;
                public static float damageXSecond = 5;
                public static float attack_radius = 2;
                public static float view_radius = 5;
                public static float velocity_max = 3;
                public static float force_max = 1000;

                public static int mC = 10;
                public static int iC = 0;
                public static int wC = 0;
            }
            public class Zombie1
            {
                public static float health_max = 50;
                public static float damageXSecond = 5;
                public static float attack_radius = 2;
                public static float view_radius = 5;
                public static float velocity_max = 2;
                public static float force_max = 1000;

            }
            public class Zombie2
            {
                public static float health_max = 300;
                public static float damageXSecond = 15;
                public static float attack_radius = 1.5f;
                public static float view_radius = 10;
                public static float velocity_max = 1;
                public static float force_max = 1000;

            }

        }

        

        public static int layer_buildings = 10;
        public static int layer_buildingThings = 9;
        public static int layer_character = 8;
        public static int layer_healthbars = 7;
    }
}

