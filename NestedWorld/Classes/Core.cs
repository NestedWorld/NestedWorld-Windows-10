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
using NestedWorldHttp.Users;
using NestedWorld.Classes.ElementsGame.Attack;
using NestedWorld.Classes.ElementsGame.Battle;
using NestedWorld.Classes.ElementsGame.Item;

namespace NestedWorld.Classes
{
    public class Core
    {

        public ItemList itemsList { get; set; } 
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
            ret = await App.network.GetObject();
            itemsList = ret.Content as ItemList;
            ret.ShowError();
            MapController = await MapController.GetNewMapController();
            Chat.Init();
            battleRouter.Init();
            user.AllyOnline = userList.UserOnLineNumber;
            user.AreasNumber = areaList.content.Count;
            user.MonsterCaptured = monsterUserList.monsterList.Count;
        }
    }
}
