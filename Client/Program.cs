using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 7000);
                Console.WriteLine("Клиент подключился");


                NetworkStream stream = client.GetStream();

               
                //реализует постоянную отпраку сообщений без разрыва
                while(true)
                {
                    Console.ReadKey();
                    string message = "сообщения от клиента";
                    byte[] byteWrite = Encoding.UTF8.GetBytes(message);
                    //какой массив отправляем, от скольки, до скольки.
                    stream.Write(byteWrite, 0, byteWrite.Length);
                    stream.Flush();
                }
                   

                    byte[] bytesRead = new byte[256];

                    stream.Read(bytesRead, 0, bytesRead.Length);
                    string answer = Encoding.UTF8.GetString(bytesRead);
                    Console.Write(answer);



                    
                
                client.Close();


                Console.WriteLine("клиент отключился");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
