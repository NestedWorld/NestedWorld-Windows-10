using NestedWorld.Classes.ElementsGame.Monsters;
using NestedWorld.View.AreaViews;
using System;
using System.Collections.Generic;
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

namespace NestedWorld.View.PrepareBattleView
{
    public sealed partial class PrepareMonsterSelected : UserControl
    {
        private List<Monster> monsterList;

        public int[] idarray { get { return IDS(); } }

        private int[] IDS()
        {
            int[] ids = new int[4] { 0, 0, 0, 0 };

            int i = 0;
            foreach (var m in MonsterListSelected.monsterList)
            {
                if (m != null)
                    ids[i] = m.ID;
                i++;
            }

            return ids;
        }

        private int _index;
        private int index
        {
            get { return _index; }
            set
            {
                _index = value;
                if (_index == 4)
                    _index = 0;
                switch (_index)
                {
                    case (0):
                        SelectOne.Begin();
                        break;
                    case (1):
                        SelectTwo.Begin();
                        break;
                    case (2):
                        SelectTree.Begin();
                        break;
                    case (3):
                        SelectFour.Begin();
                        break;
                }
            }
        }

        private void Bind()
        {
            if (monsterList[0] == null)
                itemOne.DataContext = new DataContextNull();
            else
                itemOne.DataContext = monsterList[0];
            if (monsterList[1] == null)
                itemTwo.DataContext = new DataContextNull();
            else
                itemTwo.DataContext = monsterList[1];
            if (monsterList[2] == null)
                itemThree.DataContext = new DataContextNull();
            else
                itemThree.DataContext = monsterList[2];
            if (monsterList[3] == null)
                itemFour.DataContext = new DataContextNull();
            else
                itemFour.DataContext = monsterList[3];

        }

        public MonsterList MonsterListSelected
        {
            get
            {
                MonsterList ret = new MonsterList(monsterList);
                return ret;
            }
            set { }
        }

        public PrepareMonsterSelected()
        {
            this.InitializeComponent();
            Init();
        }

        public void Init()
        {
            monsterList = new List<Monster>();
            monsterList.Add(null);
            monsterList.Add(null);
            monsterList.Add(null);
            monsterList.Add(null);
            index = 0;
            Bind();
        }

        public void Add(Monster monster)
        {
            monsterList[index] = monster;
            Bind();
            index++;
        }

    }
}
