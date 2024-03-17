using Mirror;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class AutoNetworkManager : NetworkManager
{
    public override void Start()
    {
        base.Start();
        // Automatically set the network address to the local IP address
        networkAddress = GetLocalIPAddress();
        Debug.Log("Local IP Address set to: " + networkAddress);
    }

    // Helper method to get the local IPv4 address
    private string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }
}
