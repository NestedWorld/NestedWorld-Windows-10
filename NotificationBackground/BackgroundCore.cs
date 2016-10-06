using System;
using MessagePack.Client;
using MessagePack.Serveur;
using NotificationBackground.Utils;
using Windows.ApplicationModel.Background;
using Windows.Networking.Sockets;
using MessagePack.Client.Special;
using MessagePack.Serveur.Combat;
using SocketLib;

namespace NotificationBackground
{
    public sealed class BackgroundCore : IBackgroundTask
    {
        private BackgroundTaskDeferral deferral;
        private string Token { get { return StorageApplication.GetValue("TOKEN", ""); } set { } }
        private object serveurMessageList;
        private object clientMessageList;
        private object streamConnection;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();

            Log.Info("BackgroundCore", "Run");

            Init();

        }

        private async void Init()
        {
            Log.Info("BackgroundCore", "Init");
            serveurMessageList = new ServeurMessageList();
            (serveurMessageList as ServeurMessageList).Init();
            clientMessageList = new ClientMessageList();
            streamConnection = new SocketLib.StreamConnection(new SocketLib.SocketTemplate()
            {
                hostname = "eip.kokakiwi.net",
                //hostname="localhost",
                port = 6464,
                offline = false,

            });
            (serveurMessageList as ServeurMessageList)["result"].OnCompled += (clientMessageList as ClientMessageList).ReceiveRequest;
            (serveurMessageList as ServeurMessageList)["combat:available"].OnCompled += CombatAvailableRequest;
            (streamConnection as StreamConnection).onConnect += StreamConnection_onConnect;
            (streamConnection as StreamConnection).onReceiveCompled += StreamConnection_onReceiveCompled;
            (streamConnection as StreamConnection).onSendCompled += StreamConnection_onSendCompled;
            (streamConnection as StreamConnection).onError += StreamConnection_onError;

            await (streamConnection as StreamConnection).Connect();
            Log.Info("BackgroundCore", "Init end");

        }

        private void StreamConnection_onError(Windows.Networking.Sockets.SocketErrorStatus error, Exception content)
        {
            Log.Error("SteamConnection", error.ToString() + " => " + content);
        }

        private void StreamConnection_onSendCompled()
        {
            Log.Info("StreamConnection", "PacketSend");
        }

        private void StreamConnection_onReceiveCompled(byte[] obj)
        {
            Log.Info("StreamConnection", "PacketReceive");
            (serveurMessageList as ServeurMessageList).SelectMessage(obj);
        }

       

        private void StreamConnection_onConnect()
        {
            Log.Info("StreamConnection", "Connect");
            (clientMessageList as ClientMessageList).stream = (streamConnection as StreamConnection);
            (streamConnection as StreamConnection).isConnect = true;

            (clientMessageList as ClientMessageList).SendRequest(Authenticate.GetAuthenticate(Token));
            (streamConnection as StreamConnection).StartLisen();
        }

        private void CombatAvailableRequest(object value)
        {
            Available av = value as Available;

            Log.Info("Combat oppoment", av.id);
        }
    }
}
