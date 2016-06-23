//|---------------------------------------------------------------|
//|                  NETDUNIO WEB API CLIENT                      |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                  NETDUNIO WEB API CLIENT                      |
//|---------------------------------------------------------------|
using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using Toolbox.NETMF.NET;

namespace NetduinoWebAPIClient
{
    public class Program
    {
        static OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);
        public static void Main()
        {
            try
            {
                // Creates a new web session
                HTTP_Client WebSession = new HTTP_Client(new IntegratedSocket("127.0.0.1", 80));
                int index = 1;
                while (true)
                {
                    // Requests the latest source
                    HTTP_Client.HTTP_Response Response = WebSession.Post(string.Concat("/restfulsignalrservice/messagebroadcast/broadcast?message=Hello_From_Netdunio.Index", index.ToString()));

                    // Did we get the expected response ? (a "200 OK")
                    if (Response.ResponseCode != 200)
                    {
                        AnimateLed(100);
                        Debug.Print(string.Concat("Unexpected HTTP response code : ", Response.ResponseCode.ToString()));
                        Debug.Print(Response.ToString());
                    }
                    else
                    {
                        AnimateLed(250);
                        Debug.Print("Successful response ");
                        Debug.Print(Response.ToString());
                    }
                    if (index == Int32.MaxValue)
                    {
                        index = 1;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        static void AnimateLed(int sleepTime)
        {
            led.Write(true);
            Thread.Sleep(sleepTime);
            led.Write(false);
            Thread.Sleep(sleepTime);
        }
    }
}
