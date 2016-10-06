using NestedWorld.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace NestedWorld.Classes.Network
{
    public class ReturnObject
    {
        public object Content { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }

        public override string ToString()
        {
            if (IsError)
                return "Error (" + ErrorCode.ToString() + ") : " + Message;
            return string.Empty;
        }

        public bool IsError { get { return ErrorCode != 0; } private set { } }

        public async void ShowErrorOnApp()
        {
            if (IsError)
            {
                Log.Error("Return Object", this.ToString());
                await new MessageDialog(this.ToString()).ShowAsync();
            }
        }

        public void ShowError()
        {
            if (IsError)
                Log.Error("Return Object", this.ToString());
        }
    }
}
