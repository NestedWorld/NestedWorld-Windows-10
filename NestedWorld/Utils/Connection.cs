using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace NestedWorld.Utils
{

    public class Connection
    {
        private bool _hasInternetAccess;

        public delegate void OnNoInternetAccessCallBack();
        public delegate void OnInternetAccessCallBack();

        public event OnInternetAccessCallBack OnInternetAccess;
        public event OnNoInternetAccessCallBack OnNoInternetAccess;

        public bool HasInternetAccess
        {
            get { return _hasInternetAccess; }
            set
            {
                if (value)
                    OnInternetAccess?.Invoke();
                else
                    OnNoInternetAccess?.Invoke();
                _hasInternetAccess = value;
            }
        }
        public Connection()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformationOnNetworkStatusChanged;
            CheckInternetAccess();
        }

        private void NetworkInformationOnNetworkStatusChanged(object sender)
        {
            CheckInternetAccess();
        }

        private void CheckInternetAccess()
        {
            var connectionProfile = NetworkInformation.GetInternetConnectionProfile();
            HasInternetAccess = (connectionProfile != null &&
                                 connectionProfile.GetNetworkConnectivityLevel() ==
                                 NetworkConnectivityLevel.InternetAccess);
        }
    }
}
