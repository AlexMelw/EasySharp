namespace EasySharp.NHelpers.Networking.EnlargedTypes
{
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    ///     Wrapper around TcpListener that exposes the IsActive property
    /// </summary>
    public class TcpListenerEx : TcpListener
    {
        /// <summary>
        ///     Gets a value that indicates whether <see cref="T:System.Net.Sockets.TcpListener" /> is not actively listening
        ///     for client connections.
        /// </summary>
        /// <returns>true if <see cref="T:System.Net.Sockets.TcpListener" /> is not actively listening; otherwise, false.</returns>
        public bool IsInactive => !base.Active;

        /// <summary>
        ///     Gets a value that indicates whether <see cref="T:System.Net.Sockets.TcpListener" /> is actively listening for
        ///     client connections.
        /// </summary>
        /// <returns>true if <see cref="T:System.Net.Sockets.TcpListener" /> is actively listening; otherwise, false.</returns>
        public bool IsActive => base.Active;

        #region CONSTRUCTORS

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class with the specified local
        ///     endpoint.
        /// </summary>
        /// <param name="localEP">
        ///     An <see cref="T:System.Net.IPEndPoint" /> that represents the local endpoint to which to bind the
        ///     listener <see cref="T:System.Net.Sockets.Socket" />.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="localEP" /> is null. </exception>
        public TcpListenerEx(IPEndPoint localEP) : base(localEP) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener" /> class that listens for incoming
        ///     connection attempts on the specified local IP address and port number.
        /// </summary>
        /// <param name="localaddr">An <see cref="T:System.Net.IPAddress" /> that represents the local IP address. </param>
        /// <param name="port">The port on which to listen for incoming connection attempts. </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="localaddr" /> is null. </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="port" /> is not between
        ///     <see cref="F:System.Net.IPEndPoint.MinPort" /> and <see cref="F:System.Net.IPEndPoint.MaxPort" />.
        /// </exception>
        public TcpListenerEx(IPAddress localaddr, int port) : base(localaddr, port) { }

        #endregion
    }
}