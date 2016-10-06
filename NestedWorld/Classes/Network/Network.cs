using MessagePack.Client;
using MessagePack.Client.Special;
using MessagePack.Serveur;
using MessagePack.Serveur.Combat;
using NestedWorld.Classes.ElementsGame.Attack;
using NestedWorld.Classes.ElementsGame.Monsters;
using NestedWorld.Classes.ElementsGame.Player;
using NestedWorld.Classes.ElementsGame.Users;
using NestedWorld.Classes.Network.Request.Attacks;
using NestedWorld.Classes.Network.Request.Places;
using NestedWorld.Classes.Request.Auth;
using NestedWorld.Classes.Request.Monster;
using NestedWorld.Classes.Request.Users;
using NestedWorld.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using NestedWorld.View.RootView;

namespace NestedWorld.Classes.Network
{
    public class Network
    {
        private string _token;
        public InternetStatueView connectionView;

        public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                StorageApplication.SetValue("TOKEN", value);

            }
        }

        public Utils.Connection Connection { get; set; }

        public ServeurMessageList serveurMessageList { get; set; }
        public ClientMessageList clientMessageList { get; set; }
        public SocketLib.StreamConnection streamConnection { get; set; }
        public Network()
        {
            serveurMessageList = new ServeurMessageList();
            serveurMessageList.Init();
            clientMessageList = new ClientMessageList();
            streamConnection = new SocketLib.StreamConnection(new SocketLib.SocketTemplate()
            {
                hostname = "eip.kokakiwi.net",
                //hostname="localhost",
                port = 6464,
                offline = App.core.Offline,

            });
            Connection = new Utils.Connection();
            Connection.OnNoInternetAccess += Connection_OnNoInternetAccess;
            Connection.OnInternetAccess += Connection_OnInternetAccess;
            serveurMessageList["result"].OnCompled += clientMessageList.ReceiveRequest;
            //serveurMessageList["combat:available"].OnCompled += CombatAvailableRequest;
            streamConnection.onConnect += StreamConnection_onConnect;
            streamConnection.onReceiveCompled += StreamConnection_onReceiveCompled;
            streamConnection.onSendCompled += StreamConnection_onSendCompled;
            streamConnection.onError += StreamConnection_onError;
        }

        public bool IsFirstConnection()
        {
            return true;
        }


        private void Connection_OnInternetAccess()
        {
            //throw new NotImplementedException();

            Log.Info("Connection", "Internet Access");
           // this.connectionView.Hide();
        }

        private void Connection_OnNoInternetAccess()
        {
            //throw new NotImplementedException();

            Log.Info("Connection", "No Internet Access");
            //this.connectionView.Show();
        }

        private void CombatAvailableRequest(object value)
        {
            Available av = value as Available;

            Log.Info("Combat oppoment", App.core.monsterList[av.monster_id].Name);
        }


        #region HttpRequest

        #region User
        public async Task<ReturnObject> Connect(string mail, string pass)
        {
            ReturnObject ReturnObject;

            Login login = new Login();
            login.SetParam(pass, mail);
            if (App.core.Offline)
                return new ReturnObject()
                {
                    Content = null,
                    ErrorCode = 0,
                    Message = string.Empty
                };
            try
            {
                JObject obj = await login.GetJsonAsync();
                Token = obj["token"].ToObject<string>();
                Debug.WriteLine("Token = " + Token);
                await streamConnection.Connect();
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = 0, Message = string.Empty };
            }
            catch (HttpRequestException HRException)
            {
                Debug.WriteLine(HRException);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = HRException.HResult, Message = "HttpRequestException : " + HRException.Message };
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                Debug.WriteLine(jEx);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = jEx.HResult, Message = "JsonException : " + jEx.Message };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = ex.HResult, Message = "Exception : " + ex.Message };

            }
            return ReturnObject;
        }

        public async Task<ReturnObject> GetAllies()
        {
            ReturnObject ReturnObject;

            UserFriendsGet userFriendsGet = new UserFriendsGet();
            userFriendsGet.SetParam();
            if (App.core.Offline)
                return new ReturnObject()
                {
                    Content = UserList.NewUserListOffLine(),
                    ErrorCode = 0,
                    Message = string.Empty
                };
            try
            {
                JObject obj = await userFriendsGet.GetJsonAsync();
                UserList userlist = UserList.NewUserListFromJson(obj);
                ReturnObject = new ReturnObject() { Content = userlist, ErrorCode = 0, Message = string.Empty };
            }
            catch (HttpRequestException HRException)
            {
                Debug.WriteLine(HRException);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = HRException.HResult, Message = "HttpRequestException : " + HRException.Message };
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                Debug.WriteLine(jEx);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = jEx.HResult, Message = "JsonException : " + jEx.Message };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = ex.HResult, Message = "Exception : " + ex.Message };

            }
            return ReturnObject;
        }

        public async Task<ReturnObject> PostAllies(string pseudo)
        {
            ReturnObject ReturnObject;

            UserFriendsPost userFriendsGet = new UserFriendsPost();
            userFriendsGet.SetParam(pseudo);
            if (App.core.Offline)
                return new ReturnObject()
                {
                    Content = UserList.NewUserListOffLine(),
                    ErrorCode = 0,
                    Message = string.Empty
                };
            try
            {
                JObject obj = await userFriendsGet.GetJsonAsync();

                ReturnObject = new ReturnObject() { Content = obj, ErrorCode = 0, Message = string.Empty };
            }
            catch (HttpRequestException HRException)
            {
                Debug.WriteLine(HRException);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = HRException.HResult, Message = "HttpRequestException : " + HRException.Message };
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                Debug.WriteLine(jEx);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = jEx.HResult, Message = "JsonException : " + jEx.Message };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = ex.HResult, Message = "Exception : " + ex.Message };

            }
            return ReturnObject;
        }

        public async Task<ReturnObject> GetUserInfo()
        {
            ReturnObject ReturnObject;

            if (App.core.Offline)
            {
                return new ReturnObject()
                {
                    Content = UserInfo.GetYou(),
                    ErrorCode = 0,
                    Message = string.Empty
                };
            }

            Users user = new Users();
            user.SetParam();

            try
            {
                JObject obj = await user.GetJsonAsync();
                ReturnObject = new ReturnObject()
                {
                    Content = UserInfo.GetUserInfoFromJson(obj),
                    ErrorCode = 0,
                    Message = string.Empty
                };
            }
            catch (HttpRequestException HRException)
            {
                Debug.WriteLine(HRException);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = HRException.HResult, Message = "HttpRequestException : " + HRException.Message };
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                Debug.WriteLine(jEx);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = jEx.HResult, Message = "JsonException : " + jEx.Message };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = ex.HResult, Message = "System.Exception : " + ex.Message };

            }
            return ReturnObject;
        }

        public async Task<ReturnObject> Logout()
        {
            if (App.core.Offline)
                return new ReturnObject()
                {
                    Content = null,
                    ErrorCode = 0,
                    Message = string.Empty

                };

            ReturnObject ReturnObject;
            Logout logout = new Logout();
            logout.SetParam();

            try
            {
                JObject obj = await logout.GetJsonAsync();
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = 0, Message = string.Empty };
            }
            catch (HttpRequestException HRException)
            {
                Debug.WriteLine(HRException);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = HRException.HResult, Message = "HttpRequestException : " + HRException.Message };
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                Debug.WriteLine(jEx);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = jEx.HResult, Message = "JsonException : " + jEx.Message };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = ex.HResult, Message = "Exception : " + ex.Message };

            }
            return ReturnObject;
        }
        #endregion

        #region Monster
        public async Task<ReturnObject> GetMonster(int number = -1)
        {
            ReturnObject ReturnObject;

            MonsterList ml;

            Monsters monsters = new Monsters();
            monsters.SetParam();
            if (App.core.Offline)
                return new ReturnObject()
                {
                    Content = MonsterList.GetMonsterListFromJson(null, 30),
                    ErrorCode = 0,
                    Message = string.Empty

                };
            try
            {
                ml = MonsterList.GetMonsterListFromJson(await monsters.GetJsonAsync());
                ReturnObject = new ReturnObject() { Content = ml, ErrorCode = 0, Message = string.Empty };
            }
            catch (HttpRequestException HRException)
            {
                Debug.WriteLine(HRException);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = HRException.HResult, Message = "HttpRequestException : " + HRException.Message };
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                Debug.WriteLine(jEx);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = jEx.HResult, Message = "JsonException : " + jEx.Message };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = ex.HResult, Message = "Exception : " + ex.Message };

            }
            return ReturnObject;
        }

        public async Task<ReturnObject> GetUserMonster()
        {
            ReturnObject ReturnObject;

            MonsterList ml;

            UsersMonster monsters = new UsersMonster();
            monsters.SetParam(Token);

            if (App.core.Offline)
                return new ReturnObject()
                {
                    Content = MonsterList.GetUserMonsterListFromJson(null, 12),
                    ErrorCode = 0,
                    Message = string.Empty

                };

            try
            {
                ml = MonsterList.GetUserMonsterListFromJson(await monsters.GetJsonAsync());
                ReturnObject = new ReturnObject() { Content = ml, ErrorCode = 0, Message = string.Empty };
            }
            catch (HttpRequestException HRException)
            {
                Debug.WriteLine(HRException);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = HRException.HResult, Message = "HttpRequestException : " + HRException.Message };
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                Debug.WriteLine(jEx);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = jEx.HResult, Message = "JsonException : " + jEx.Message };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = ex.HResult, Message = "Exception : " + ex.Message };

            }
            return ReturnObject;
        }

        public async Task<ReturnObject> GetMonsterAttack(int IdMonster)
        {
            ReturnObject ReturnObject;

            MonsterAttackIDGet request = new MonsterAttackIDGet();
            request.SetParam(IdMonster);


            if (App.core.Offline)
            {
                return new ReturnObject()
                {
                    Content = null,
                    ErrorCode = 0,
                    Message = ""
                };
            }

            try
            {
                var jsontmp = await request.GetJsonAsync();

                ReturnObject = new ReturnObject() { Content = jsontmp, ErrorCode = 0, Message = "" };
                return ReturnObject;
            }
            catch (HttpRequestException HRException)
            {
                Debug.WriteLine(HRException);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = HRException.HResult, Message = "HttpRequestException : " + HRException.Message };
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                Debug.WriteLine(jEx);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = jEx.HResult, Message = "JsonException : " + jEx.Message };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = ex.HResult, Message = "Exception : " + ex.Message };

            }
            return ReturnObject;
        }

        #endregion

        #region Attacks

        public async Task<ReturnObject> GetAttack()
        {
            ReturnObject ReturnObject;

            AttacksGet request = new AttacksGet();
            request.SetParam();


            if (App.core.Offline)
            {
                return new ReturnObject()
                {
                    Content = null,
                    ErrorCode = 0,
                    Message = ""
                };
            }

            try
            {
                var jsontmp = await request.GetJsonAsync();

                ReturnObject = new ReturnObject() { Content = AttackList.LoadFromJson(jsontmp), ErrorCode = 0, Message = "" };
                return ReturnObject;
            }
            catch (HttpRequestException HRException)
            {
                Debug.WriteLine(HRException);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = HRException.HResult, Message = "HttpRequestException : " + HRException.Message };
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                Debug.WriteLine(jEx);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = jEx.HResult, Message = "JsonException : " + jEx.Message };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = ex.HResult, Message = "Exception : " + ex.Message };

            }
            return ReturnObject;
        }


        #endregion

        #region geo

        public async Task<ReturnObject> GetPlaces(int PlaceID = -1)
        {
            ReturnObject ReturnObject;

            PlaceGet request = new PlaceGet();
            request.SetParam(PlaceID);


            if (App.core.Offline)
            {
                return new ReturnObject()
                {
                    Content = null,
                    ErrorCode = 0,
                    Message = ""
                };
            }

            try
            {
                var jsontmp = await request.GetJsonAsync();

                ReturnObject = new ReturnObject();
            }
            catch (HttpRequestException HRException)
            {
                Debug.WriteLine(HRException);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = HRException.HResult, Message = "HttpRequestException : " + HRException.Message };
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                Debug.WriteLine(jEx);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = jEx.HResult, Message = "JsonException : " + jEx.Message };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = ex.HResult, Message = "System.Exception : " + ex.Message };

            }
            return ReturnObject;
        }

        public async Task<ReturnObject> GetPlaceMonster(int PlaceID)
        {
            ReturnObject ReturnObject;

            PlaceMonster request = new PlaceMonster();
            request.SetParam(PlaceID);


            if (App.core.Offline)
            {
                return new ReturnObject()
                {
                    Content = null,
                    ErrorCode = 0,
                    Message = ""
                };
            }

            try
            {
                var jsontmp = await request.GetJsonAsync();

                ReturnObject = new ReturnObject();
            }
            catch (HttpRequestException HRException)
            {
                Debug.WriteLine(HRException);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = HRException.HResult, Message = "HttpRequestException : " + HRException.Message };
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                Debug.WriteLine(jEx);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = jEx.HResult, Message = "JsonException : " + jEx.Message };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = ex.HResult, Message = "Exception : " + ex.Message };

            }
            return ReturnObject;
        }

        public async Task<ReturnObject> GetRegionMonster(int regionID)
        {
            ReturnObject ReturnObject;

            RegionMonster request = new RegionMonster();
            request.SetParam(regionID);


            if (App.core.Offline)
            {
                return new ReturnObject()
                {
                    Content = null,
                    ErrorCode = 0,
                    Message = ""
                };
            }

            try
            {
                var jsontmp = await request.GetJsonAsync();

                ReturnObject = new ReturnObject();
            }
            catch (HttpRequestException HRException)
            {
                Debug.WriteLine(HRException);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = HRException.HResult, Message = "HttpRequestException : " + HRException.Message };
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                Debug.WriteLine(jEx);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = jEx.HResult, Message = "JsonException : " + jEx.Message };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = ex.HResult, Message = "Exception : " + ex.Message };

            }
            return ReturnObject;
        }


        public async Task<ReturnObject> GetRegion()
        {
            ReturnObject ReturnObject;

            RegionsGet request = new RegionsGet();
            request.SetParam();


            if (App.core.Offline)
            {
                return new ReturnObject()
                {
                    Content = null,
                    ErrorCode = 0,
                    Message = ""
                };
            }

            try
            {
                var jsontmp = await request.GetJsonAsync();

                ReturnObject = new ReturnObject();
            }
            catch (HttpRequestException HRException)
            {
                Debug.WriteLine(HRException);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = HRException.HResult, Message = "HttpRequestException : " + HRException.Message };
            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                Debug.WriteLine(jEx);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = jEx.HResult, Message = "JsonException : " + jEx.Message };
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
                ReturnObject = new ReturnObject() { Content = null, ErrorCode = ex.HResult, Message = "Exception : " + ex.Message };

            }
            return ReturnObject;
        }

        #endregion

        #endregion

        #region LiveRequest

        public void SendRequest(RequestBase request)
        {
            clientMessageList.SendRequest(request);
        }

        private void StreamConnection_onError(Windows.Networking.Sockets.SocketErrorStatus error, System.Exception content)
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
            serveurMessageList.SelectMessage(obj);
        }

        private void StreamConnection_onConnect()
        {
            Log.Info("StreamConnection", "Connect");
            clientMessageList.stream = this.streamConnection;
            streamConnection.isConnect = true;
            SendRequest(Authenticate.GetAuthenticate(Token));
            streamConnection.StartLisen();
        }


        #endregion
    }
}
