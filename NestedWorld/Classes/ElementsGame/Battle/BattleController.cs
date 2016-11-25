using NestedWorld.View.BattleViews;
using System;
using System.Collections.Generic;
using Windows.UI.Input.Inking;
using Windows.UI.Xaml;
using MessagePack.Serveur.Combat;
using NestedWorld.Classes.ElementsGame.Monsters;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using MessagePackNestedWorld.MessagePack.Serveur.Combat;

namespace NestedWorld.Classes.ElementsGame.Battle
{
    public class BattleController
    {
        public List<BattleIcon> iconList { get; set; }

        public UI.AnimationCanvas annimationCanvas { get; set; }

        private List<Patern> paternList;

        public Start start { get { return _start; } set { _start = value; this.round = value.first; } }
        public int combatID;
        private Monster _ennemieMonster;
        private Start _start;
        private Monster _userMonster;

        public Battle self { get; set; }

        public bool round { get; set; }


        public Monsters.MonsterList UserMonsters { get; set; }

        public Monsters.Monster UserMonster
        {
            get { return _userMonster; }
            set
            {
                _userMonster = value;
                UIUserMonster.Monster = value;
            }
        }
        public Monsters.Monster EnnemieMonster
        {
            get { return _ennemieMonster; }
            set
            {
                _ennemieMonster = value;
                UIEnnemiMonster.Monster = value;
            }
        }

        public EnemieMonster UIEnnemiMonster { get; set; }
        public UserMonster UIUserMonster { get; set; }

        public BattleController()
        {
            paternList = new List<Patern>();
            paternList.Add(new Paterns.AttackPatern(this));
            paternList.Add(new Paterns.DefencePatern(this));
            paternList.Add(new Paterns.UseToolsPatern(this));
            paternList.Add(new Paterns.AttackSpePatern(this));
            paternList.Add(new Paterns.DefenceSpePatern(this));

            UIEnnemiMonster = null;
            UIUserMonster = null;
        }


        public void Init(BattleCanvas value)
        {
            iconList = new List<BattleIcon>();
            iconList.Add(new BattleIcon("ms-appx:///Assets/disk.png", 1));
            iconList.Add(new BattleIcon("ms-appx:///Assets/disk.png", 2));
            iconList.Add(new BattleIcon("ms-appx:///Assets/disk.png", 3));
            iconList.Add(new BattleIcon("ms-appx:///Assets/disk.png", 4));
            iconList.Add(new BattleIcon("ms-appx:///Assets/disk.png", 5));
            iconList.Add(new BattleIcon("ms-appx:///Assets/disk.png", 6));
        

            foreach (BattleIcon b in iconList)
            {
                value.mainCanvas.Children.Add(b);
            }

            App.network.serveurMessageList["combat:attack-received"].OnCompled += AttackReceiveEvent;
            App.network.serveurMessageList["combat:monster-ko"].OnCompled += MonsterKoEvent;
            App.network.serveurMessageList["combat:end"].OnCompled += EndEvent;
            App.network.serveurMessageList["combat:monster-replaced"].OnCompled += MonsterReplacedEvent;


            value.inkCanvas.InkPresenter.StrokeInput.StrokeEnded += StrokeInput_StrokeEnded;
            value.inkCanvas.InkPresenter.StrokeInput.StrokeStarted += StrokeInput_StrokeStarted;

            SetPlacement(Window.Current.Bounds.Height, Window.Current.Bounds.Width);
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void MonsterReplacedEvent(object value)
        {
            MonsterReplaced monsterReplaced = (value as MonsterReplaced);


            this.EnnemieMonster = App.core.monsterList[monsterReplaced.monster.Monster_Id].FromStruct(monsterReplaced.monster);
            //monsterReplaced.monster
        }

        #region Event

        private async void EndEvent(object value)
        {
            End end = value as End;
            // end battle

            Utils.Log.Info("end combat");
            await new MessageDialog("you " + end.Status, "Combat finish").ShowAsync();

            Frame root = Window.Current.Content as Frame;
            root.Navigate(typeof(Pages.HomePage));
        }

        private void MonsterKoEvent(object value)
        {
            MonsterKo monsterko = value as MonsterKo;

            //annim victoire :)
        }

        private void AttackReceiveEvent(object value)
        {
            AttackReceived attackReceived = value as AttackReceived;
            Utils.Log.Info(attackReceived.Monster);
            try
            {
                if (attackReceived.Monster.Id == start.OppomentMonster.Id)
                {
                    //oppoment monster send;
                    this.round = true;
                    UserMonster = UserMonster.FromStruct(attackReceived.Target);
                    EnnemieMonster = EnnemieMonster.FromStruct(attackReceived.Monster);
                    annimationCanvas.Sprite = App.core.Resources.AttackSprite[App.core.attackList.list[attackReceived.Attack].AttackRessourcesName];
                }
                else if (attackReceived.Monster.Id == start.UserMonster.Id)
                {
                    this.round = false;
                    UserMonster = UserMonster.FromStruct(attackReceived.Monster);
                    EnnemieMonster = EnnemieMonster.FromStruct(attackReceived.Target);
                    annimationCanvas.Sprite = App.core.Resources.AttackSprite[App.core.attackList.list[attackReceived.Attack].AttackRessourcesName];
                }

                //TODO : tmp code; 
                if (EnnemieMonster.Life <= 0)
                {
                    //oppoment monster KO
                }
                if (UserMonster.Life <= 0)
                {
                    //user monster KO;
                }
              
            }
            catch (System.Exception ex)
            {
                Utils.Log.Error("AttackReceived", ex);
            }
        }

        #endregion


        #region UI
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
            double alpha = (2 * Math.PI) / 6;

            double centerX = height / 2;
            double centerY = width / 2;



            double defaultTop = (height / 2) - (iconList[0].Height / 2);
            double defaultLeft = ((width) / 2) - (iconList[0].Height / 2);

            int index = 0;

            foreach (BattleIcon item in iconList)
            {


                item.top = ((Math.Sin(PidivTwo + index * alpha)) * 150) + defaultTop - 200;
                item.left = ((Math.Cos(PidivTwo + index * alpha)) * 150) + defaultLeft;
                index++;

            }
        }

        #endregion
    }
}
