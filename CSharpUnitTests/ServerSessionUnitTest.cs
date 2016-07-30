using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.ClientServer.Fakes;
using Model;
using Model.ClientServer;

namespace SSGUnitTestProj
{
    [TestClass]
    public class ServerSessionUnitTest
    {
        [TestMethod]
        [TestCategory("CheckIn")]
        public void TestStartSession()
        {
            string str = "init";
            StubINetworkServerService IServiceStub = new StubINetworkServerService()
            {
                StartMeAsServerString = (obj) => { str += "_startMeAsServer"; },
                PublishToRecognizeMySessionString = (obj)=> { str += "_publish"; },                            

            };
            ServerSession ss = new ServerSession(IServiceStub);
            Assert.AreEqual(ss.State, SessionState.eNone);

            string strPlayername = "sachin";
            ss.StartSession(strPlayername);
            //check state
            Assert.AreEqual(ss.State, SessionState.eWaitingRoom);
            //check if dependencies get executed in right order
            Assert.AreEqual("init_publish_startMeAsServer", str);
            //Test if they have added itself as user while satrtiong the session
            Assert.AreEqual(ss.PlayerList.Count, 1);
            Assert.AreEqual(ss.PlayerList[0].IPAddress, Utility.GetLocalIPAddress().ToString());            
        }

        [TestMethod]
        [TestCategory("CheckIn")]
        public void TestPlayGame()
        {
            string str = "init";
            StubINetworkServerService IServiceStub = new StubINetworkServerService()
            {

                StopRecognizingNow = () => { str += "_stopRecognizingMe"; }                

            };
            ServerSession ss = new ServerSession(IServiceStub);
            Assert.AreEqual(ss.State, SessionState.eNone);
            ss.PlayGame();
            Assert.AreEqual(ss.State, SessionState.eRunningGame);
            //check if dependencies get executed in right order
            Assert.AreEqual("init_stopRecognizingMe", str);        

        }

        [TestMethod]
        [TestCategory("CheckIn")]
        public void TestCloseSession()
        {
            string str = "init";
            StubINetworkServerService IServiceStub = new StubINetworkServerService()
            {
                
                BroadcastCommandPacket = (obj) =>{ str += "_broadcasting"; },
                CloseServerCommCleanly = () => { str += "_closeCleanly"; }               

            };
            ServerSession ss = new ServerSession(IServiceStub);
            ss.StopSession();
            //check if dependencies get executed in right order
            Assert.AreEqual("init_broadcasting_closeCleanly", str);
            //Test if they have added itself as user while satrtiong the session
            Assert.AreEqual(ss.PlayerList.Count, 0);
            
        }


    }
}
