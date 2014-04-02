using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Threading;
namespace GaeaPlayVideo
{
    class DataController
    {
        public bool isExit = false;
        public Buffers teleBuffers = null;
        public Form1 mainWindow = null;

        UdpClient udpClient = new UdpClient(11000);

        public DataController()
        {
            try
            {
                udpClient.Connect(IPAddress.Parse("192.168.1.252"), 11000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void start()
        {
            new Thread(listen).Start();
        }

        public void stop()
        {
            isExit = false;
        }

        public void listen()
        {
            // Sends a message to the host to which you have connected.
            //          Byte[] sendBytes = Encoding.ASCII.GetBytes("Is anybody there?");

            //          udpClient.Send(sendBytes, sendBytes.Length);

            // Sends a message to a different host using optional hostname and port parameters.
            //UdpClient udpClientB = new UdpClient();
            //udpClientB.Send(sendBytes, sendBytes.Length, "AlternateHostMachineName", 11000);

            //IPEndPoint object will allow us to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            // Blocks until a message returns on this socket from a remote host.
            while (true)
            {
                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                int temp = 0;
                Bitmap pic = new Bitmap(10, 10);
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 10; j++)
                    {
                        pic.SetPixel(i, j, Color.FromArgb(receiveBytes[temp * 3], receiveBytes[temp * 3 + 1], receiveBytes[temp * 3 + 2]));
                        temp++;
                    }
                teleBuffers.write2Buffers(pic);
                if (isExit) break;
            }

            // Uses the IPEndPoint object to determine which of these two hosts responded.

            udpClient.Close();
            //udpClientB.Close();


        }
    }
}
