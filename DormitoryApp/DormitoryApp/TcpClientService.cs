using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryApp
{
    public class TcpClientService
    {
        private const string ServerAddress = "127.0.0.1";
        private const int ServerPort = 8008;

        public async Task<string> SendRequestAsync(string request)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    await client.ConnectAsync(ServerAddress, ServerPort);
                    if (!client.Connected)
                    {
                        return "ERROR|Не вдалося підключитися до сервера.";
                    }

                    byte[] data = Encoding.UTF8.GetBytes(request);
                    NetworkStream stream = client.GetStream();

                    await stream.WriteAsync(data, 0, data.Length);

                    byte[] buffer = new byte[4096];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    return response;
                }
            }
            catch (SocketException)
            {
                return "ERROR|Помилка підключення: Сервер не запущено.";
            }
            catch (Exception ex)
            {
                return $"ERROR|Загальна помилка: {ex.Message}";
            }
        }
    }
}