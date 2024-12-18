using System.Net;
using System.Net.Sockets;
using Network.Core.TCP.Exceptions;
using Network.Core.TCP.TCP;

namespace Network.TCP
{
    public class TcpClient : ITcpClient
    {
        // Private properties
        private IPEndPoint? _localEndpoint;
        private IPEndPoint? _RemoteEndpoint;
        private Socket? _client;
        private NetworkStream _stream;

        private bool _disposed;

        // Public properties
        public bool Connected
        {
            get => _client != null && _client.Connected;
        }

        public int Available
        {
            get => _client.Available;
        }

        public IPEndPoint? LocalEndpoint
        {
            get => _localEndpoint ?? _client.LocalEndPoint as IPEndPoint;
        }

        public IPEndPoint? RemoteEndPoint
        {
            get => _RemoteEndpoint ?? _client.RemoteEndPoint as IPEndPoint;
        }

        public int ReceiveBufferSize
        {
            get => _client.ReceiveBufferSize;
            set { _client.ReceiveBufferSize = value; }
        }

        public int SendBufferSize
        {
            get => _client.SendBufferSize;
            set { _client.SendBufferSize = value; }
        }


        public TcpClient(Socket socket)
        {
            _client = socket;
        }

        public TcpClient(IPEndPoint endPoint)
        {
            _localEndpoint = endPoint;

            _InitializeClientSocket();
        }


        public NetworkStream GetStream()
        {
            _VerifyActiveConnected();

            if (_stream is null)
            {
                _stream = new NetworkStream(_client);
            }

            return _stream;
        }

        public void Connect(string hostname, int port)
        {
            _client.Connect(hostname, port);
        }

        public async Task ConnectAsync(string hostname, int port)
        {
            await _client.ConnectAsync(hostname, port);
        }

        public void Disconnect()
        {
            _VerifyActiveConnected();

            _stream.Dispose();
            _client.Disconnect(true);

            _stream = null;
        }

        private void _VerifyActiveConnected()
        {
            if (Connected is false)
            {
                throw new ConnectionNotEstablishedException("The connection was not established.");
            }
        }

        private void _InitializeClientSocket()
        {
            if (_localEndpoint is null)
            {
                _client = new Socket(SocketType.Stream, ProtocolType.Tcp);
                return;
            }

            _client = new Socket(_localEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _stream.Dispose();
                _client.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            Dispose();
        }

        ~TcpClient()
        {
            this.Dispose(false);
        }
    }
}