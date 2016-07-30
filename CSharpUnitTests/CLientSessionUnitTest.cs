using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.ClientServer.Fakes;
using Model;

namespace SSGUnitTestProj
{
    [TestClass]
    public class CLientSessionUnitTest
    {
        [TestMethod]
        [TestCategory("CheckIn")]
        public void TestJoinSession()
        {
            string str = "init";
            string strPlayerName = "vishal";
            StubINetworkClientService IServiceStub = new StubINetworkClientService()
            {
                JoinServerStringString = (obj,obj2) => { str += "_joining_" + obj + "_" + obj2 + "_"; },
                InitiateListening = ()=> { str += "listening"; }

            };

            string strIP = "10.10.10.10";
            ClientSession ss = new ClientSession(IServiceStub);
            Assert.AreEqual(ss.State, SessionState.eNone);
            ss.JoinSession(strIP, strPlayerName);
            Assert.AreEqual(ss.State, SessionState.eWaitingRoom);
            Assert.AreEqual(ss.MyInfo.Name, strPlayerName);
            string strExpected = "init_joining_" + strIP + "_"+strPlayerName+ "_listening";
            Assert.AreEqual(strExpected, str);
           
        }
        [TestMethod]
        [TestCategory("CheckIn")]
        public void TestSendInfoToServer()
        {
            string str = "init_";
            StubINetworkClientService IServiceStub = new StubINetworkClientService()
            {
                SendInfoToServerString = (obj) => { str += obj; }

            };

            
            ClientSession ss = new ClientSession(IServiceStub);            
            ss.SendInfoToServer("Info");

            string strExpected = "init_Info";
            Assert.AreEqual(strExpected, str);

        }
        [TestMethod]
        [TestCategory("CheckIn")]
        public void TestStopClientSession()
        {
            string str = "init_";
            StubINetworkClientService IServiceStub = new StubINetworkClientService()
            {
                CloseClientCleanly = () => { str += "ClosingNetwrokService"; }

            };

            
            ClientSession ss = new ClientSession(IServiceStub);
            ss.StopSession();

            string strExpected = "init_ClosingNetwrokService";
            Assert.AreEqual(strExpected, str);
            Assert.AreEqual(ss.State, SessionState.eNone);

        }
    }
}
