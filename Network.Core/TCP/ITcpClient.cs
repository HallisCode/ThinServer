using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Network.Core.TCP.TCP
{
    public interface ITcpClient : IDisposable
    {
        /// <summary>
        /// Установлено ли соединение с другим ендпоинтом.
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// Сколько байт еще доступно на чтение.
        /// </summary>
        int Available { get; }

        /// <summary>
        /// Еднпоинт на котором работает сокет.
        /// </summary>
        IPEndPoint? LocalEndpoint { get; }

        /// <summary>
        /// Ендпоинт с которым должно быть соединение.
        /// </summary>C
        IPEndPoint? RemoteEndPoint { get; }

        /// <summary>
        /// Размер буфера в байтах на входящие данные.
        /// </summary>
        int ReceiveBufferSize { get; set; }

        /// <summary>
        /// Размер буфера в байтах на исходящие данные.
        /// </summary>
        int SendBufferSize { get; set; }

        /// <summary>
        /// Поток текущего соединения для передачи данных.
        /// <exception cref="ConnectionNotEstablishedException"></exception>
        /// </summary>
        NetworkStream GetStream();
        
        /// <summary>
        /// Подключиться к сокету.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        void Connect(string hostname, int port);

        /// <summary>
        /// Подключиться к сокету.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        Task ConnectAsync(string hostname, int port);

        /// <summary>
        /// Закрывает текущее соединение.
        /// <exception cref="ConnectionNotEstablishedException"></exception>
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Закрывает текущее соединение и освобождает ресурсы.
        /// </summary>
        void Close();
    }
}