using NestedWorld.Classes.ElementsGame.Player;
using NestedWorld.Classes.ElementsGame.Users;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View.PrepareBattleView
{
    public sealed partial class UsersProfileEntrance : UserControl
    {
        public static readonly DependencyProperty UserBackgroundImageSourceProperty = DependencyProperty.Register("UserBackgroundImageSource", typeof(string), typeof(UsersProfileEntrance), null);
        public static readonly DependencyProperty EnemieBackgroundImageSourceProperty = DependencyProperty.Register("EnemieBackgroundImageSource", typeof(string), typeof(UsersProfileEntrance), null);

        public static readonly DependencyProperty UserImageSourceProperty = DependencyProperty.Register("UserImageSource", typeof(string), typeof(UsersProfileEntrance), null);
        public static readonly DependencyProperty EnemieImageSourceProperty = DependencyProperty.Register("EnemieImageSource", typeof(string), typeof(UsersProfileEntrance), null);

        public static readonly DependencyProperty UserLevelProperty = DependencyProperty.Register("UserLevel", typeof(string), typeof(UsersProfileEntrance), null);
        public static readonly DependencyProperty EnemieLevelProperty = DependencyProperty.Register("EnemieLevel", typeof(string), typeof(UsersProfileEntrance), null);

        public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register("UserName", typeof(string), typeof(UsersProfileEntrance), null);
        public static readonly DependencyProperty EnemieNameProperty = DependencyProperty.Register("EnemieName", typeof(string), typeof(UsersProfileEntrance), null);


        public string UserBackground
        {
            get { return GetValue(UserBackgroundImageSourceProperty).ToString(); }
            set { SetValue(UserBackgroundImageSourceProperty, value); }
        }

        public string EnemieBackground
        {
            get { return GetValue(EnemieBackgroundImageSourceProperty).ToString(); }
            set { SetValue(EnemieBackgroundImageSourceProperty, value); }
        }

        public string UserImage
        {
            get { return GetValue(UserImageSourceProperty).ToString(); }
            set { SetValue(UserImageSourceProperty, value); }
        }

        public string EnemieImage
        {
            get { return GetValue(EnemieImageSourceProperty).ToString(); }
            set { SetValue(EnemieImageSourceProperty, value); }
        }

        public int UserLevel
        {
            get { return 0; }
            set { SetValue(UserLevelProperty, value.ToString() + " lvl"); }
        }
        public int EnemieLevel
        {
            get { return 0; }
            set { SetValue(EnemieLevelProperty, value.ToString() + " lvl"); }
        }

        public string UserName
        {
            get { return ""; }
            set { SetValue(UserNameProperty, value); }
        }

        public string EnemieName
        {
            get { return ""; }
            set { SetValue(EnemieNameProperty, value); }
        }


        public UserInfo User
        {
            get
            {
                return null;
            }
            set
            {
                UserImage = value.Image;
                UserBackground = value.Background;
                UserName = value.Name;
                UserLevel = value.Level;
            }
        }

        public User Enemie
        {
            get { return null; }
            set
            {
                EnemieImage = value.Image;
                EnemieBackground = value.Background;
                EnemieName = value.Name;
                EnemieLevel = value.Level;
            }
        }

        public UsersProfileEntrance()
        {
            this.InitializeComponent();
            this.DataContext = this;
            Show.Completed += Show_Completed;
        }

        private void Show_Completed(object sender, object e)
        {
            Leave.Begin();
        }

        public void Begin()
        {
            Show.Begin();
        }

    }
}
