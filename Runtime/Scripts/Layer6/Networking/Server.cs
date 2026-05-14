#region

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using IbrahKit.Debugging;
using IbrahKit.Manager;
using UnityEngine;

#endregion

namespace IbrahKit
{
    public class Server : Manager_Local<Server>
    {
#if !UNITY_WEBGL

        private UdpClient udpClient;

        private IPEndPoint remoteEndPoint;

        private Thread receiveThread;

        public Action<string> OnMessageRecieved;

        [SerializeField] private int sendPort;
        [SerializeField] private int recievePort;

        protected override void InstanceAwake()
        {
            base.InstanceAwake();
            
            try
            {
                udpClient = new()
                {
                    EnableBroadcast = true
                };

                remoteEndPoint = new(IPAddress.Broadcast, sendPort);

                ThreadStart threadStart = new(ReceiveResponses);
                
                receiveThread = new(threadStart)
                {
                    IsBackground = true
                };

                receiveThread.Start();
            }
            catch
            {

            }
        }

        protected override void InstanceDestroy()
        {
            base.InstanceDestroy();
            
            receiveThread.Abort();

            udpClient.Close();
        }

        public new void BroadcastMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);

            udpClient.Send(data, data.Length, remoteEndPoint);
        }

        private void ReceiveResponses()
        {
            try
            {
                UdpClient responseClient = new(recievePort);

                IPEndPoint clientEndPoint = new(IPAddress.Any, 0);

                while (true)
                {
                    byte[] data = responseClient.Receive(ref clientEndPoint);

                    string response = Encoding.UTF8.GetString(data);

                    IbrahDebug.Log("Received response from client: " + response);

                    OnMessageRecieved?.Invoke(response);
                }
            }
            catch
            {

            }
        }
#endif
    }
}