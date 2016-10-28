using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace SocketLib
{
    public class StreamConnection
    {
        private const int BUFFSIZE = 512;

        public bool isConnect;

        #region CallbackDeffinition
        public delegate void CallbackOnConnect();
        public delegate void CallbackOnSendCompled();
        public delegate void CallbackOnReceiveCompled(byte[] obj);

        public delegate void CallbackOnError(SocketErrorStatus error, Exception content);

        #endregion

        #region Callback
        public event CallbackOnConnect onConnect;
        public event CallbackOnSendCompled onSendCompled;
        public event CallbackOnReceiveCompled onReceiveCompled;

        public event CallbackOnError onError;
        #endregion

        private CoreDispatcher dispatcher { get { return Window.Current.Dispatcher; } set { } }
        private DataWriter writer;


        public SocketTemplate template { get; set; }
        public StreamSocket stream { get; set; }

        public StreamConnection(SocketTemplate template)
        {
            this.template = template;
            stream = new StreamSocket();
            writer = null;
        }

        public async Task Connect()
        {

            if (template.offline)
            {
                onConnect?.Invoke();
                return;
            }
            try
            {
                await stream.ConnectAsync(template.HostName, template.port.ToString());
                onConnect?.Invoke();

            }
            catch (Exception ex)
            {
                Debug.Write("Connect");

                onError?.Invoke(SocketError.GetStatus(ex.HResult), ex);
            }
        }


        public async void StartLisen()
        {
            try
            {
                Debug.WriteLine("Start Lisen");
                DataReader reader = new DataReader(stream.InputStream);

                reader.InputStreamOptions = InputStreamOptions.Partial;
                reader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                var count = await reader.LoadAsync(BUFFSIZE);
                byte[] type = new byte[1];
                while (isConnect)
                {
                    uint byteCount = reader.UnconsumedBufferLength;
                    byte[] bytes = new byte[byteCount];
                    if (byteCount == 0)
                        Disconnect();
                    if (byteCount == 1)
                    {
                        reader.ReadBytes(type);
                        if (byteCount == 0)
                            Disconnect();

                        count = await reader.LoadAsync(BUFFSIZE);

                        byteCount = reader.UnconsumedBufferLength;
                        bytes = new byte[byteCount];
                        reader.ReadBytes(bytes);

                        var tmp = type.Concat(bytes).ToArray<byte>();
                        onReceiveCompled?.Invoke(tmp);
                    }
                    else
                    {
                        reader.ReadBytes(bytes);
                        onReceiveCompled?.Invoke(bytes);
                    }
                    Debug.WriteLine("lisen...");
                    count = await reader.LoadAsync(BUFFSIZE);
                }
            }
            catch (Exception ex)
            {
                Debug.Write("Lisen");
                onError?.Invoke(SocketError.GetStatus(ex.HResult), ex);
            }

        }

        public void Send(MemoryStream data)
        {
            Send(data.ToArray());
        }

        public async void Send(byte[] data)
        {
            if (writer == null)
                writer = new DataWriter(stream.OutputStream);
            try
            {
                writer.WriteBytes(data);
                Utils.PrintBytes(data);

                await writer.StoreAsync();
                onSendCompled?.Invoke();
            }
            catch (Exception ex)
            {
                onError?.Invoke(SocketError.GetStatus(ex.HResult), ex);
            }
        }

        public void Disconnect()
        {
            isConnect = false;
            stream.Dispose();
            Debug.WriteLine("Disconnect");
        }

    }
}
