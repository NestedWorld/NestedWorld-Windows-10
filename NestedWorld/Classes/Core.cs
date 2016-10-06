using NestedWorld.Classes.ElementsGame.Player;
using NestedWorld.Classes.ElementsGame.Maps;
using NestedWorld.Classes.ElementsGame.Monsters;
using NestedWorld.Classes.ElementsGame.Shop;
using NestedWorld.Classes.ElementsGame.Users;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using NestedWorld.Classes.DesignUtilities;
using NestedWorld.ViewModel;
using NestedWorld.Classes.ElementsGame.Chat;
using NestedWorld.Utils;
using NestedWorld.Classes.Request.Users;
using NestedWorld.Classes.ElementsGame.Attack;
using NestedWorld.Classes.ElementsGame.Battle;

namespace NestedWorld.Classes
{
    public class Core
    {
        public BattleRouter battleRouter { get; set; }
        public MonsterList monsterList { get; set; }
        public AttackList attackList { get; set; }
        public MonsterList monsterUserList { get; set; }
        public NotificationInternal notificationInternal { get; set; }

        public Resources Resources { get; set; }

        public UserList userList { get; set; }
        public AreaList areaList { get; set; }

        public UserInfo user { get; set; }

        public bool Offline { get; set; }

        public Shop Shop { get; set; }
        public ChatCore Chat { get; set; }
        public MapController MapController { get; set; }
        public Core()
        {
            Offline = false;
            userList = new UserList();
            Resources = new Resources();
            monsterList = new MonsterList();
            monsterUserList = new MonsterList();
            userList = new UserList();
            areaList = new AreaList();
            Shop = new Shop();
            battleRouter = new BattleRouter();
            Chat = new ChatCore();
            notificationInternal = new NotificationInternal();
            Log.Info("Device", Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily);
            Log.Info("BUILD", Settings.Settings.GetAppVersion());
        }


        public async void ShowError(string ErrorMessage)
        {
            if (ErrorMessage == string.Empty)
                return;
            var messageDialog = new MessageDialog(ErrorMessage);
            await messageDialog.ShowAsync();
        }

        public async Task Init()
        {
            //UpdateInfo();
            areaList.Init();
            var ret = await App.network.GetUserInfo();
            user = ret.Content as UserInfo;
            ret.ShowError();
            ret = await App.network.GetAttack();
            attackList = ret.Content as AttackList;
            ret.ShowError();
            ret = await App.network.GetMonster();
            monsterList = ret.Content as MonsterList;
            ret.ShowError();
            ret = await App.network.GetUserMonster();
            monsterUserList = ret.Content as MonsterList;
            ret.ShowError();
            ret = await App.network.GetAllies();
            userList = ret.Content as UserList;
            ret.ShowError();
            MapController = await MapController.GetNewMapController();
            Chat.Init();
            battleRouter.Init();
            user.AllyOnline = userList.UserOnLineNumber;
            user.AreasNumber = areaList.content.Count;
            user.MonsterCaptured = monsterUserList.monsterList.Count;
        }

        private async void UpdateInfo()
        {
            UserPut up = new UserPut();

            up.SetParam("Thomas Caron", "https://scontent-cdg2-1.xx.fbcdn.net/v/t1.0-9/10922803_10208019930037378_4351704227098544776_n.jpg?oh=9a17c7043f72507a08a4a9aa5e5eacb7&oe=57EE46D7", "https://scontent-cdg2-1.xx.fbcdn.net/v/t1.0-9/12745856_10208904941482111_44201625354049998_n.jpg?oh=a788c33b1d88bac244cafdaab88cb7b9&oe=57FF34FA");

            await up.GetJsonAsync();
        }
    }
}
