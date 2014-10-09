using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public static class Constants
    {
        public static readonly float MinVelocity = 0.05f;

        /// <summary>
        /// The collider of the panda is bigger when on idle state
        /// This will make it easier for the user to tap on it
        /// </summary>
        public static readonly float PandaColliderRadiusNormal = 0.235f;
        public static readonly float PandaColliderRadiusBig = 0.5f;
    }
}
