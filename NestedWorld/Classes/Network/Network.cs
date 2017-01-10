using MessagePack.Client;
using MessagePack.Client.Special;
using MessagePack.Serveur;
using MessagePack.Serveur.Combat;
using NestedWorld.Classes.ElementsGame.Attack;
using NestedWorld.Classes.ElementsGame.Monsters;
using NestedWorld.Classes.ElementsGame.Player;
using NestedWorld.Classes.ElementsGame.Users;
using NestedWorldHttp;
using NestedWorldHttp.Attacks;
using NestedWorldHttp.Places;
using NestedWorldHttp.Auth;
using NestedWorldHttp.Monster;
using NestedWorldHttp.Users;
using NestedWorld.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using NestedWorld.View.RootView;
using NestedWorldHttp.Http.Users;
using NestedWorld.Classes.ElementsGame.Item;
using NestedWorldHttp.Http.Places;
using NestedWorld.Classes.ElementsGame.Portals;
using NestedWorldHttp.Http.Monster;
using NestedWorld.Classes.ElementsGame.Exchanges;

namespace NestedWorld.Classes.Network
{
    public class Network
    {
        public InternetStatueView connectionView;

        public string Token
        {
            get
            {
                return App.UserSession.Token;
            }
            set
            {
                App.UserSession.Token = value;
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
                port = 6465,
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
                var ret = await login.GetJsonAsync();
                if (ret.code == System.Net.HttpStatusCode.OK)
                {
                    Token = ret.Object["token"].ToObject<string>();
                    App.UserSession.SaveToken();
                    await streamConnection.Connect();
                    ReturnObject = new ReturnObject() { Content = null, ErrorCode = 0, Message = string.Empty };
                }
                else
                {
                    ReturnObject = new ReturnObject() { Content = ret.Object["message"].ToObject<string>(), ErrorCode = 1, Message = ret.Object["message"].ToObject<string>() };
                }
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

        public async Task<ReturnObject> UpdateInfo(string name, string userImage, string userBackground)
        {
            ReturnObject ReturnObject;
            UserPut up = new UserPut();

            try
            {
                up.SetParam(name, userImage, userBackground);
                var ret = await up.GetJsonAsync();

                if (ret.code == System.Net.HttpStatusCode.OK)
                {
                    ReturnObject = new ReturnObject() { Content = UserInfo.GetUserInfoFromJson(ret.Object), ErrorCode = 0, Message = ret.Object["message"].ToObject<string>() };
                }
                else
                {
                    ReturnObject = new ReturnObject() { Content = null, ErrorCode = 1, Message = "Error updating user info" };
                }
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

        public async Task<ReturnObject> UpdateMonsterInfo(int id, string surname)
        {
            ReturnObject ReturnObject;
            PutUserMonster up = new PutUserMonster();

            try
            {
                up.SetParam(id, surname);
                var ret = await up.GetJsonAsync();

                if (ret.code == System.Net.HttpStatusCode.OK)
                {
                    ReturnObject = new ReturnObject() { Content = Monster.GetUserMonster(ret.Object), ErrorCode = 0, Message = ret.Object["message"].ToObject<string>() };
                }
                else
                {
                    ReturnObject = new ReturnObject() { Content = null, ErrorCode = 1, Message = "Error updating user info" };
                }
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

        public async Task<ReturnObject> GetUser(int id)
        {
            ReturnObject ReturnObject;
            GetUserId getUserId = new GetUserId();
            try
            {
                getUserId.SetParam(id);
                var ret = await getUserId.GetJsonAsync();
                ReturnObject = new ReturnObject() { Content = User.GetFronJson(ret.Object), ErrorCode = 0, Message = string.Empty };
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

        public async Task<ReturnObject> GetUserStat(int id = -1)
        {
            ReturnObject ReturnObject;
            GetUserStats GetUserStats = new GetUserStats();
            try
            {
                if (id == -1)
                    GetUserStats.SetParam();
                else
                    GetUserStats.SetParam(id);
                var ret = await GetUserStats.GetJsonAsync();
                ReturnObject = new ReturnObject() { Content = ElementsGame.Users.Stats.LoadJson(ret.Object), ErrorCode = 0, Message = string.Empty };
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
                var ret = await userFriendsGet.GetJsonAsync();
                UserList userlist = UserList.NewUserListFromJson(ret.Object);
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

        public async Task<ReturnObject> GetUserInventory()
        {
            ReturnObject ReturnObject;

            NestedWorldHttp.Inventory.UserInventory getObject = new NestedWorldHttp.Inventory.UserInventory();
            getObject.SetParam();

            try
            {
                var ret = await getObject.GetJsonAsync();
                Inventory itemList = Inventory.NewJromJson(ret.Object);

                ReturnObject = new ReturnObject() { Content = itemList, ErrorCode = 0, Message = string.Empty };
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

        public async Task<ReturnObject> GetObject()
        {
            ReturnObject ReturnObject;

            NestedWorldHttp.Inventory.ObjectGet getObject = new NestedWorldHttp.Inventory.ObjectGet();
            getObject.SetParam();

            try
            {
                var ret = await getObject.GetJsonAsync();
                ItemList itemList = ItemList.NewJromJson(ret.Object);

                ReturnObject = new ReturnObject() { Content = itemList, ErrorCode = 0, Message = string.Empty };
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

            UserFriendsPost userFriendsPost = new UserFriendsPost();
            userFriendsPost.SetParam(pseudo);
            if (App.core.Offline)
                return new ReturnObject()
                {
                    Content = UserList.NewUserListOffLine(),
                    ErrorCode = 0,
                    Message = string.Empty
                };
            try
            {
                var ret = await userFriendsPost.GetJsonAsync();
                if (ret.code == System.Net.HttpStatusCode.OK)
                {
                    return await this.GetAllies();
                }
                else
                {
                    ReturnObject = new ReturnObject() { Content = ret, ErrorCode = 1, Message = string.Empty };

                }
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

            Me user = new Me();
            user.SetParam();

            try
            {
                var ret = await user.GetJsonAsync();
                ReturnObject = new ReturnObject()
                {
                    Content = UserInfo.GetUserInfoFromJson(ret.Object),
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
                var ret = await logout.GetJsonAsync();
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
                var ret = await monsters.GetJsonAsync();
                ml = MonsterList.GetMonsterListFromJson(ret.Object);
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
                var ret = await monsters.GetJsonAsync();
                ml = MonsterList.GetUserMonsterListFromJson(ret.Object);
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

                ReturnObject = new ReturnObject() { Content = jsontmp.Object, ErrorCode = 0, Message = "" };
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


        #region exchange

        public async Task<ReturnObject> GetExchange()
        {
            ReturnObject ReturnObject;
            NestedWorldHttp.Http.Exchange.GetExchanges getExchanges = new NestedWorldHttp.Http.Exchange.GetExchanges();

            try
            {
                getExchanges.SetParam();
                var ret = await getExchanges.GetJsonAsync();

                if (ret.code == System.Net.HttpStatusCode.OK)
                {
                    ReturnObject = new ReturnObject() { Content = ExchangeList.LoadJson(ret.Object), ErrorCode = 0, Message = "" };
                }
                else
                {
                    ReturnObject = new ReturnObject() { Content = null, ErrorCode = 1, Message = "Error updating user info" };
                }
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

        public async Task<ReturnObject> PostEchange(Exchange exchange)
        {
            ReturnObject ReturnObject;
            NestedWorldHttp.Http.Exchange.PostExchanges postExchanges = new NestedWorldHttp.Http.Exchange.PostExchanges();

            try
            {
                postExchanges.SetParam(exchange.MonsterIdSend, exchange.MonsterIdAsk, exchange.UserMonsterSend.UserID);
                var ret = await postExchanges.GetJsonAsync();

                if (ret.code == System.Net.HttpStatusCode.OK)
                {
                    ReturnObject = new ReturnObject() { Content = Exchange.LoadJson(ret.Object), ErrorCode = 0, Message = "" };
                }
                else
                {
                    ReturnObject = new ReturnObject() { Content = null, ErrorCode = 1, Message = "Error updating user info" };
                }
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

        public async Task<ReturnObject> AcceptEchange(Exchange exchange)
        {
            ReturnObject ReturnObject;
            NestedWorldHttp.Http.Exchange.PostEchangesAccept postExchanges = new NestedWorldHttp.Http.Exchange.PostEchangesAccept();

            try
            {
                postExchanges.SetParam(exchange.Id, exchange.UserMonsterSend.UserID);
                var ret = await postExchanges.GetJsonAsync();

                if (ret.code == System.Net.HttpStatusCode.OK)
                {
                    ReturnObject = new ReturnObject() { Content = ret.Object, ErrorCode = 0, Message = "" };
                }
                else
                {
                    ReturnObject = new ReturnObject() { Content = null, ErrorCode = 1, Message = "Error updating user info" };
                }
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
                var ret = await request.GetJsonAsync();
                if (ret.code != System.Net.HttpStatusCode.OK)
                    ReturnObject = new ReturnObject() { Content = "", ErrorCode = Convert.ToInt32(ret.code), Message = "" };
                else
                    ReturnObject = new ReturnObject() { Content = AttackList.LoadFromJson(ret.Object), ErrorCode = 0, Message = "" };

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
                var ret = await request.GetJsonAsync();

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

        public async Task<ReturnObject> GetPortals(double lat, double lon)
        {
            ReturnObject ReturnObject;

            PortailGet request = new PortailGet();
            request.SetParam(lat, lon);


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
                ReturnObject = new ReturnObject() { Content = PortalList.LoadJson(jsontmp.Object), ErrorCode = 0, Message = "" };
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
