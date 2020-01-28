using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KlientServert
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //через какой порт стучаться к нам
                //PAddress.Any - получить запрос откуда угодно.
                TcpListener serverSoket = new TcpListener(IPAddress.Any, 7000);
                Console.WriteLine("Старт сервера");
                serverSoket.Start();
                
                while(true)
                {

                   

                    //ставим сервер на ожидание.
                    TcpClient clientSoket = serverSoket.AcceptTcpClient();
                    //чтобы отвечать клиенту
                    NetworkStream stream = clientSoket.GetStream();

                    //постоянно слушает соединения.
                    while(true)
                    {
                        byte[] bytes = new byte[256];
                        stream.Read(bytes, 0, bytes.Length);
                        string request = Encoding.UTF8.GetString(bytes);
                        Console.WriteLine("ответ \n" + request);

                        string message = "Длина сообщения" + request.Length;
                        bytes = Encoding.UTF8.GetBytes(message);
                        //какой массив отправляем, от скольки, до скольки.
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                    }
                    

                   // clientSoket.Close();

                }


                
                serverSoket.Stop();

                Console.WriteLine("Сервер стоп");
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();


        }
    }
}
