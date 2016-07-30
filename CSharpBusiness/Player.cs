using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum PlayerState
    {
        eHandlerDisplayer, //Handler Busy; Displayer Busy
        eHandler, //only Handler Busy
        eDisplayer, //Only Displayer Busy
        eFree,  //All free
        eNone, //None for random use
    }
    public class Player
    {
        public Player(string ip, string name = "")
        {
            IPAddress = ip;
            Name = name;
            State = PlayerState.eFree;
        }
        public string Name { get; set; }
        public string IPAddress { get; set; }
        public PlayerState State { get; set; }

    }
}
