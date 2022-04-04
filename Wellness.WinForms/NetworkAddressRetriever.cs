using System.Net;
using System.Text.RegularExpressions;

namespace Wellness.WinForms
{
    internal class NetworkAddressRetriever
    {
        public IEnumerable<string> NetworkAddresses
        {
            get
            {
                var addresses = Dns.GetHostEntry(Dns.GetHostName())
                    .AddressList.Select(e =>
                    {
                        var addressBytes = e.GetAddressBytes();
                        Func<byte[], string> ipv6ToHex = bytes =>
                        {
                            var hexed = bytes.Select(b => b.ToString("X2")).ToList();
                            var paired = new List<string>();
                            for (var i = 0; i < hexed.Count >> 1; i++)
                            {
                                paired.Add(hexed[i] + hexed[i + 1]);
                            }

                            var deZeroed = paired.Select(pair => new Regex("^0{1,3}").Replace(pair, ""));
                            var res = string.Join(':', deZeroed);
                            var index = res.IndexOf("0:0", StringComparison.Ordinal);
                            return index < 0 ? res : new Regex("0(:0)+").Replace(res, "::", 1);
                        };
                        return e.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6
                            ? ipv6ToHex(addressBytes)
                            : string.Join('.', addressBytes.Select(w => w));
                    });
                return addresses;
            }
        }
    }
}
