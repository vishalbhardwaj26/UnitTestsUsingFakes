
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model.ClientServer
{
    public interface INetworkServerService
    {
        #region server related
        /// <summary>
        /// When user join to server
        /// </summary>
        event EventHandler OnUserJoined;

        event EventHandler OnDataReciedFromClient;
        /// <summary>
        /// Keep publishing some packets, to be recognized by others to join
        /// Will be called while creating new session
        /// </summary>        
        void PublishToRecognizeMySession(string playerName);
        /// <summary>
        /// We can call it to stop udp packets , which was being sent to be recognized
        /// </summary>
        void StopRecognizingNow();

        /// <summary>
        /// It would listen to the users, who joined, and subsequently open up the chnnel with joined users for
        /// further communication
        /// </summary>
        void StartMeAsServer(string playerName);
        /// <summary>
        /// Broadcast required packet to all the clients
        /// </summary>
        /// <param name="cmdPkt"></param>
        void Broadcast(CommandPacket cmdPkt);

        /// <summary>
        /// Close server cleanly
        /// </summary>
        void CloseServerCommCleanly();

        #endregion
    }
    public interface INetworkClientService
    {
        /// <summary>
        /// On Any data recieved from Server
        /// </summary>
        event EventHandler OnDataReceived;

        /// <summary>
        /// Start and Keep Listening from Server
        /// </summary>
        void InitiateListening();
        
        /// <summary>
        /// Find all created server sessions
        /// </summary>
        /// <returns></returns>
        Dictionary<IPAddress,string> FindAllSessions();

        string HostName();

        /// <summary>
        /// Join Particular Server
        /// </summary>
        /// <param name="IPAddress"></param>
        void JoinServer(string IPAddress,string playerName);

        /// <summary>
        /// Send Any Information back to server
        /// </summary>
        /// <param name="strInfo"></param>
        void SendInfoToServer(string strInfo);

        /// <summary>
        /// CLosing CLient cleanly
        /// </summary>
        void CloseClientCleanly();


    }

    public class UserArg : EventArgs
    {
        public string strUserID;
        public IPAddress ipAddress;

        public UserArg(string strid, IPAddress ip = null)
        {
            strUserID = strid;
            ipAddress = ip;

        }
    }
    public class ClientsDataEventArgs : EventArgs
    {
        public string DataTransmitted;
        public ClientsDataEventArgs(string _Data)
        {
            DataTransmitted = _Data;
        }

    }
    public class CommandPacket
    {
        public CommandPacket(Player plyDisp, Player plyHandl, string cmd)
        {
            Displayer = plyDisp;
            Displayer = plyHandl;
            Command = cmd;
        }
        public Player Displayer { get; set; }
        public Player Handler { get; set; }
        public string Command { get; set; }
    }
}
