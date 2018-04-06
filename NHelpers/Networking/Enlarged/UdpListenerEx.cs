namespace EasySharp.NHelpers.Networking.Enlarged
{
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    ///     <para>Wrapper around UdpClient that exposes the Active property.</para>
    ///     <para>
    ///         UdpListenerEx also differentiates UdpClient in a more visual manner stating that it is explicitly used for
    ///         listening purposes.
    ///     </para>
    /// </summary>
    class UdpListenerEx : UdpClient
    {
        /// <summary>Gets or sets a value indicating whether a default remote host has been established.</summary>
        /// <returns>
        ///     <see langword="true" /> if a connection is active; otherwise, <see langword="false" />.
        /// </returns>
        public new bool Active => base.Active;

        /// <summary>Gets or sets a value indicating whether a default remote host has not been established.</summary>
        /// <returns>
        ///     <see langword="false" /> if a connection is active; otherwise, <see langword="true" />.
        /// </returns>
        public bool Inactive => !base.Active;

        #region CONSTRUCTORS

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpClient" /> class and binds it to the
        ///     specified local endpoint.
        /// </summary>
        /// <param name="localEP">
        ///     An <see cref="T:System.Net.IPEndPoint" /> that represents the local endpoint to which you bind
        ///     the UDP connection.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="localEP" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="T:System.Net.Sockets.SocketException">
        ///     An error occurred when accessing the socket. See the Remarks
        ///     section for more information.
        /// </exception>
        public UdpListenerEx(IPEndPoint localEP) : base(localEP) { }

        #endregion
    }
}