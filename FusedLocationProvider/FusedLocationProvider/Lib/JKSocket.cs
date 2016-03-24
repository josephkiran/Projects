using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FusedLocationProvider.Lib
{
    public class StateObject
    {
        internal byte[] sBuffer;
        internal Socket sSocket;
        internal StateObject(int size, Socket sock)
        {
            sBuffer = new byte[size];
            sSocket = sock;
        }
    }

    public class JKSocket
    {
        string _ipAddress;
        int _port;
        public Action<ConnectionStatus, string> StatusEvent { get; set; }

        public Action<string> RecdDataEvent { get; set; }

        private Socket _clientSocket;
        public bool IsServer { get { return _isServer; } }
        private bool _isServer = true;
        public JKSocket(string ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
            _isServer = false;
        }

        public JKSocket()
        {
            _isServer = true;
        }

        public void StartServer()
        {
            if (!_isServer) throw new Exception("Not a server");
            IPAddress ipAddress =
          Dns.Resolve(Dns.GetHostName()).AddressList[0];

            IPEndPoint ipEndpoint =
              new IPEndPoint(ipAddress, 1800);

            Socket listenSocket =
              new Socket(AddressFamily.InterNetwork,
                         SocketType.Stream,
                         ProtocolType.Tcp);

            listenSocket.Bind(ipEndpoint);
            listenSocket.Listen(1);
            IAsyncResult asyncAccept = listenSocket.BeginAccept(
              new AsyncCallback(AcceptCallback),
              listenSocket);
        }

        public void ConnectToServer()
        {
            if (_isServer) throw new Exception("Not a client");

            IPAddress ipAddress =
          Dns.Resolve(_ipAddress).AddressList[0];

            IPEndPoint ipEndpoint =
              new IPEndPoint(ipAddress, _port);

            Socket clientSocket = new Socket(
              AddressFamily.InterNetwork,
              SocketType.Stream,
              ProtocolType.Tcp);

            IAsyncResult asyncConnect = clientSocket.BeginConnect(
              ipEndpoint,
              new AsyncCallback(connectCallback),
              clientSocket);
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            //allDone.Set();

            // Get the socket that handles the client request.
            Socket sc = (Socket)ar.AsyncState;
            _clientSocket = sc.EndAccept(ar);

            if (_clientSocket.Connected == false)
            {
                Console.WriteLine(".client is not connected.");
                StatusEvent(ConnectionStatus.Disconnected, "disconnected");
                return;
            }
            else
            {
                Console.WriteLine(".client is connected.");
                StatusEvent(ConnectionStatus.Connected, "connected");
                BeginRecieving(_clientSocket);
            }


        }

        private void connectCallback(IAsyncResult asyncConnect)
        {
            _clientSocket =
              (Socket)asyncConnect.AsyncState;
            _clientSocket.EndConnect(asyncConnect);
            // arriving here means the operation completed
            // (asyncConnect.IsCompleted = true) but not
            // necessarily successfully
            if (_clientSocket.Connected == false)
            {
                Console.WriteLine(".client is not connected.");
                StatusEvent(ConnectionStatus.Disconnected, "disconnected");
                return;
            }
            else
            {
                Console.WriteLine(".client is connected.");
                StatusEvent(ConnectionStatus.Connected, "connected");
                BeginRecieving(_clientSocket);
            }


            //writeDot(asyncSend);
        }

        public void SendData(string msg)
        {
            byte[] sendBuffer = Encoding.ASCII.GetBytes(msg);
            IAsyncResult asyncSend = _clientSocket.BeginSend(
              sendBuffer,
              0,
              sendBuffer.Length,
              SocketFlags.None,
              new AsyncCallback(sendCallback),
              _clientSocket);

            Console.Write("Sending data.");
        }

        public void Stop()
        {
            _clientSocket.Shutdown(SocketShutdown.Both);
            _clientSocket.Close();
        }

        private void BeginRecieving(Socket sk)
        {
            StateObject stateObject =
             new StateObject(16, sk);
            // this call passes the StateObject because it
            // needs to pass the buffer as well as the socket
            IAsyncResult asyncReceive =
              _clientSocket.BeginReceive(
                stateObject.sBuffer,
                0,
                stateObject.sBuffer.Length,
                SocketFlags.None,
                new AsyncCallback(receiveCallback),
                stateObject);

            Console.Write("Receiving response.");
            //writeDot(asyncReceive);
        }


        private void receiveCallback(IAsyncResult asyncReceive)
        {
            StateObject stateObject =
             (StateObject)asyncReceive.AsyncState;

            int bytesReceived =
              stateObject.sSocket.EndReceive(asyncReceive);

            RecdDataEvent(Encoding.ASCII.GetString(stateObject.sBuffer));

            BeginRecieving(stateObject.sSocket);
        }

        private void sendCallback(IAsyncResult asyncSend)
        {
            Socket clientSocket = (Socket)asyncSend.AsyncState;
            int bytesSent = clientSocket.EndSend(asyncSend);
            StatusEvent(ConnectionStatus.DataSent, bytesSent.ToString());
            Console.WriteLine(
              ".{0} bytes sent.",
              bytesSent.ToString());

        }

    }

    public enum ConnectionStatus
    {
        Connected,
        Disconnected,
        DataSent,
        DataRecd
    }
}
