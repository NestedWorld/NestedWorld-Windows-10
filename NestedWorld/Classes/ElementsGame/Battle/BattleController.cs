using NestedWorld.View.BattleViews;
using System;
using System.Collections.Generic;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using MessagePack.Serveur.Combat;
using NestedWorld.Classes.ElementsGame.Monsters;
using Windows.UI.Xaml.Controls;

namespace NestedWorld.Classes.ElementsGame.Battle
{
    public class BattleController
    {
        public List<BattleIcon> iconList { get; set; }

        public AnnimationCanvas annimationCanvas { get; set; }

        private List<Patern> paternList;
        private int _userMIndex;
        private int _ennMIndex;
        public Start start { get; set; }
        public int combatID;
        private Monster _ennemieMonster;
        private EnemieMonster _UIEnnemiMonster;

        public Battle self { get; set; }

        public int UserIndex
        {
            get { return _userMIndex; }
            set
            {
                _userMIndex = value;
                if (UIUserMonster != null)
                    UIUserMonster.DataContext = UserMonster;
                UserMonster.LifeMax = UserMonster.Life;
            }
        }

        public int EnnemieIndex
        {
            get { return _ennMIndex; }
            set
            {
                _ennMIndex = value;
                if (UIEnnemiMonster != null)
                    UIEnnemiMonster.DataContext = EnnemieMonster;
            }
        }

        public Monsters.MonsterList UserMonsters { get; set; }
        // public Monsters.MonsterList EnnemieMonsters { get; set; }

        public Monsters.Monster UserMonster { get { return UserMonsters.monsterList[_userMIndex]; } set { } }
        public Monsters.Monster EnnemieMonster
        {
            get { return _ennemieMonster; }
            set
            {
                _ennemieMonster = value;
                value.LifeMax = value.Life;
            }
        }

        public EnemieMonster UIEnnemiMonster
        {
            get { return _UIEnnemiMonster; }
            set
            {
                _UIEnnemiMonster = value;
            }
        }
        public UserMonster UIUserMonster { get; set; }

        public BattleController()
        {
            paternList = new List<Patern>();
            paternList.Add(new Paterns.AttackPatern(this));
            paternList.Add(new Paterns.DefencePatern(this));
            paternList.Add(new Paterns.UseToolsPatern(this));
            paternList.Add(new Paterns.AttackSpePatern(this));
            paternList.Add(new Paterns.DefenceSpePatern(this));


            _ennMIndex = 0;
            _userMIndex = 0;
            UIEnnemiMonster = null;
            UIUserMonster = null;
        }


        public void Init(BattleCanvas value)
        {
            EnnemieIndex = 0;
            UserIndex = 0;

            iconList = new List<BattleIcon>();
            iconList.Add(new BattleIcon("ms-appx:///Assets/Monster_Min.png", 1));
            iconList.Add(new BattleIcon("ms-appx:///Assets/Monster_Min.png", 2));
            iconList.Add(new BattleIcon("ms-appx:///Assets/Monster_Min.png", 3));
            iconList.Add(new BattleIcon("ms-appx:///Assets/Monster_Min.png", 4));
            iconList.Add(new BattleIcon("ms-appx:///Assets/Monster_Min.png", 5));
            iconList.Add(new BattleIcon("ms-appx:///Assets/Monster_Min.png", 6));

            App.network.serveurMessageList["combat:attack-received"].OnCompled += AttackReceiveEvent;
            App.network.serveurMessageList["combat:monster-ko"].OnCompled += MonsterKoEvent;
            App.network.serveurMessageList["combat:end"].OnCompled += EndEvent;

            foreach (BattleIcon b in iconList)
            {
                value.mainCanvas.Children.Add(b);
            }
            value.inkCanvas.InkPresenter.StrokeInput.StrokeEnded += StrokeInput_StrokeEnded;
            value.inkCanvas.InkPresenter.StrokeInput.StrokeStarted += StrokeInput_StrokeStarted;
            SetPlacement(Window.Current.Bounds.Height, Window.Current.Bounds.Width);
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void EndEvent(object value)
        {
            End end = value as End;
        }

        private void MonsterKoEvent(object value)
        {
            MonsterKo monsterko = value as MonsterKo;

            //annim victoire :)
        }

        private void AttackReceiveEvent(object value)
        {
            AttackReceived attackReceived = value as AttackReceived;

            annimationCanvas.Next = App.core.attackList.list[attackReceived.Attack];
            UserMonster.Life = attackReceived.Target.Hp;
            EnnemieMonster.Life = attackReceived.Monster.Hp;
            UIUserMonster.Life = UserMonster.LifePercent;
            UIEnnemiMonster.Life = EnnemieMonster.LifePercent;

            if (EnnemieMonster.Life <= 0)
            {
                Frame root = Window.Current.Content as Frame;
                root.Navigate(typeof(Pages.EndBattlePage), EnnemieMonster);
            }
        }

        private void StrokeInput_StrokeEnded(Windows.UI.Input.Inking.InkStrokeInput sender, Windows.UI.Core.PointerEventArgs args)
        {
            sender.InkPresenter.StrokeContainer.Clear();
        }

        private void StrokeInput_StrokeStarted(Windows.UI.Input.Inking.InkStrokeInput sender, Windows.UI.Core.PointerEventArgs args)
        {
            IReadOnlyList<InkStroke> currentStrokes = sender.InkPresenter.StrokeContainer.GetStrokes();
            foreach (InkStroke inkStroke in currentStrokes)
            {
                List<int> patern = new List<int>();
                foreach (InkPoint point in inkStroke.GetInkPoints())
                {
                    foreach (var i in iconList)
                    {
                        if (i.IsOnIt(point.Position))
                        {
                            if (!patern.Contains(i.number))
                                patern.Add(i.number);
                        }
                    }
                }
                foreach (Patern p in paternList)
                {
                    if (p.isThisPatern(patern))
                    {
                        p.Execute();
                        break;
                    }
                }
            }
            sender.InkPresenter.StrokeContainer.Clear();
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            SetPlacement(e.Size.Height, e.Size.Width);
        }

        private void SetPlacement(double height, double width)
        {

            double PidivTwo = (Math.PI / 2);
            double alpha = (2 * Math.PI) / iconList.Count;
            double defautTop = ((height) / 2) - (iconList[0].Height / 2);
            double defautLeft = ((width) / 2) - (iconList[0].Width / 2);
            int index = 0;
            foreach (BattleIcon item in iconList)
            {
                item.top = ((Math.Sin(PidivTwo + index * alpha)) * 130) + (defautTop / 2) - 40;
                item.left = ((Math.Cos(PidivTwo + index * alpha)) * 130) + defautLeft;
                index++;
            }
        }
    }
}
