using System.Net;
using System.Net.Sockets;
using ThinServer.TCP.Exceptions;

namespace ThinServer.TCP
{
    /// <summary>
    /// Интерфейс TCP listener.
    /// </summary>
    public interface ITcpListener : IDisposable
    {
        /// <summary>
        /// Эндпойнт который должен прослушиваться.
        /// </summary>
        IPEndPoint LocalEndpoint { get; }

        /// <summary>
        /// Слушает ли сервер входящие соединения.
        /// </summary>
        bool Active { get; }

        /// <summary>
        /// Запуск прослушивания ендпойнта.
        /// </summary>
        void Start();

        /// <summary>
        /// Принимает входящее соединение.
        /// </summary>
        /// <exception cref="ServerNotActive"></exception>
        /// <returns></returns>
        ITcpClient AcceptTcpClient();

        /// <summary>
        /// Принимает входящее соединение.
        /// </summary>
        /// <exception cref="ServerNotActive"></exception>
        /// <returns></returns>
        Task<ITcpClient> AcceptTcpClientAsync();

        /// <summary>
        /// Прекращение прослушивания ендпойнта.
        /// </summary>
        void Stop();

        /// <summary>
        /// Прекращение прослушивания ендпойнта и освобождение ресурсов.
        /// </summary>
        void Close();
    }
}