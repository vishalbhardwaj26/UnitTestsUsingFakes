using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ClientServer;

namespace Model
{


    public abstract class Session : ISession
    {
        public Player MyInfo { get; set; }
        public string SessionID { get; set; }
        public SessionState State { get; set; }
    }
    public class ServerSession : Session, IServerSession
    {
        public event EventHandler OnUserJoined;
        INetworkServerService _serverService;
        List<Player> _Users;
        public List<Player> PlayerList { get { return _Users; } }


        public ServerSession(INetworkServerService na)
        {
            _serverService = na;
            State = SessionState.eNone;
            _Users = new List<Player>();
            //_oEngine = null;
        }

        public INetworkServerService ServerService
        {
            get
            {
                return _serverService;
            }
        }

        //GamingEngine _oEngine;
        public void PlayGame()
        {
            _serverService.StopRecognizingNow();
            State = SessionState.eRunningGame;
            //Call everbody to start
            //Run Command engine to fecth and publish command packets

          //  if (_oEngine != null)
          //      _oEngine.StopEngine();
            // _oEngine = new GamingEngine(this);
            //_oEngine.ExecuteEngine();
         
        }

        public void StartSession(string playerName)
        {
            State = SessionState.eWaitingRoom;            
            

            _serverService.OnUserJoined += NetworkAdapter_OnUserJoined;
            _serverService.OnDataReciedFromClient += _serverService_OnDataReciedFromClient;

            //Add me as host
            MyInfo = new Player(Utility.GetLocalIPAddress().ToString(), playerName);
            _Users.Add(MyInfo);
            if (OnUserJoined != null)
            {
                OnUserJoined(this, new UserArg(MyInfo.Name, null));
            }

            //publish to be recognized
            _serverService.PublishToRecognizeMySession(playerName);
            //start to keep listening if any user want to join and further communication
            _serverService.StartMeAsServer(playerName);


        }

        private void _serverService_OnDataReciedFromClient(object sender, EventArgs e)
        {
            //chnge state of player if any
           // throw new NotImplementedException();
        }

        private void NetworkAdapter_OnUserJoined(object sender, EventArgs e)
        {
            if (_Users.Count < 4)
            {
                UserArg ua = (UserArg)e;
                string strIP = ua.ipAddress == null ? "" : ua.ipAddress.ToString();
                Player ply = new Player(strIP, ua.strUserID);
                _Users.Add(ply);

                if (OnUserJoined != null)
                {
                    OnUserJoined(this, null);
                }
            }
        }

        public void StopSession()
        {
            //broadcast that closing this session
            _serverService.Broadcast(null);
            _Users.Clear();
            _serverService.CloseServerCommCleanly();
            //Stop Service
            //Exit
        }
    }

    public class ClientSession : Session, IClientSession
    {
        INetworkClientService _clientService;
        public ClientSession(INetworkClientService na)
        {
            _clientService = na;
            State = SessionState.eNone;
        }

        public Player Host
        {
            get; set;
        }

        public event EventHandler OnDataReceived;

        public void JoinSession(string strIP,string playerName)
        {
            MyInfo = new Player(Utility.GetLocalIPAddress().ToString(), playerName);
            _clientService.OnDataReceived += NetworkAdapter_DataReceived;
            _clientService.JoinServer(strIP, playerName);
            _clientService.InitiateListening();
            State = SessionState.eWaitingRoom;
        }

        public void SendInfoToServer(string strInfo)
        {
            _clientService.SendInfoToServer(strInfo);
        }
        public void StopSession()
        {
            _clientService.CloseClientCleanly();
            State = SessionState.eNone;
            //Stop Service
            //Exit
        }
        private void NetworkAdapter_DataReceived(object sender, EventArgs e)
        {
            ClientsDataEventArgs ua = (ClientsDataEventArgs)e;
            string strIP = ua.DataTransmitted;
            Player ply = new Player(strIP, ua.DataTransmitted);
            Host = ply;
            OnDataReceived(this, e);
        }
    }

}
