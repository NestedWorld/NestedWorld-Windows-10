using NestedWorld.Classes.ElementsGame.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace NestedWorld.View
{
    public sealed partial class UserView : UserControl
    {

        public delegate void AddUser();
        public event AddUser Add;
        public UserList userList
        {
            get { return null; }
            set
            {
                if (value.userList != null)
                    userGridView.DataContext = new ObservableCollection<User>(value.userList);
            }
        }
        public UserView()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Add?.Invoke();
        }
    }
}
