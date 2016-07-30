using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ClientServer;

namespace Model
{
    public enum SessionState
    {
        eWaitingRoom,
        eRunningGame,
        eNone  //random
    }

    public interface ISession
    {
        Player MyInfo { get; set; }
        string SessionID { get; set; }
        SessionState State { get; set; }
    }
    public interface IServerSession : ISession
    {
        event EventHandler OnUserJoined;

        List<Player> PlayerList { get; }
        void StartSession(string playerName);
        void StopSession();
        void PlayGame();
    }
    public interface IClientSession : ISession
    {
        event EventHandler OnDataReceived;
        void JoinSession(string strIP,string playerName);

        Player Host { get; set; }

        
        void SendInfoToServer(string strInfo);
        void StopSession();
    }
    
}
