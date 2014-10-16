using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    //enums for the state of the slingshot, the 
    //state of the game and the state of the panda
    public enum CannonState
    {
        Idle,
        UserPulling,
        PandaFlying
    }

    public enum GameState
    {
        Start,
        PandaMovingToCannon,
        Playing,
        Won,
        Lost
    }


    public enum PandaState
    {
        BeforeThrown,
        Thrown
    }
    
}
