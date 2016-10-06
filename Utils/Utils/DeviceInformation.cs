using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;

namespace NestedWorld.Utils
{
    public class DeviceInformation
    {
        public bool IsDesktop { get { return Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop"; } set { } }

        public bool IsMobile { get { return Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile"; } set { } }

        public bool IsXbox { get { return true; } set { } }
        public bool IsTablette { get { return true; } set { } }
        public bool AsKeyboard { get { return true; } set { } }
        public bool AsMousse { get { return true; } set { } }
        public bool AsJoysick { get { return true; } set { } }
        public bool AsTouch { get { return true; } set { }  }
    }
}
