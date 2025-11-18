using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryServer
{
    class Program
    {
        private static DatabaseHelper _dbHelper;

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            _dbHelper = new DatabaseHelper();
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1"); 
                int port = 8008; 
                server = new TcpListener(localAddr, port);

                server.Start();
                Console.WriteLine("Сервер запущено на 127.0.0.1:8008...");
                Console.WriteLine("Очікування на підключення...");

                while (true)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    Console.WriteLine("Клієнт підключився!");

#pragma warning disable 4014
                    Task.Run(() => HandleClient(client));
#pragma warning restore 4014
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine($"SocketException: {e}");
            }
            finally
            {
                server?.Stop();
            }
        }

        private static async void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[4096];
            string response = "ERROR|Невідомий запит.";

            try
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                Console.WriteLine($"Отримано запит: {request}");

                string[] parts = request.Split('|');
                string command = parts[0];

                try
                {
                    switch (command)
                    {
                        case "LOGIN": // "LOGIN|user|pass"
                            response = _dbHelper.ValidateUser(parts[1], parts[2]);
                            break;
                        case "REGISTER": // "REGISTER|user|pass|FullName"
                            response = _dbHelper.RegisterUser(parts[1], parts[2], parts[3]);
                            break;
                        case "SUBMIT_APP": // "SUBMIT_APP|login|fullName|studentId|reason"
                            response = _dbHelper.SubmitApplication(parts[1], parts[2], parts[3], parts[4]);
                            break;
                        case "GET_STATUS": // "GET_STATUS|login"
                            response = _dbHelper.GetApplicationStatus(parts[1]);
                            break;
                        case "GET_APP_DATA": // "GET_APP_DATA|login"
                            response = _dbHelper.GetApplicationData(parts[1]);
                            break;
                        case "UPDATE_APP": // "UPDATE_APP|login|fullName|studentId|reason"
                            response = _dbHelper.UpdateApplication(parts[1], parts[2], parts[3], parts[4]);
                            break;
                        case "GET_ALL_APPS": // "GET_ALL_APPS"
                            response = _dbHelper.GetAllApplications();
                            break;
                        case "UPDATE_APP_STATUS": // "UPDATE_APP_STATUS|IdЗаявки|НовийСтатус"
                            int appId = int.Parse(parts[1]);
                            string newStatus = parts[2]; 
                            response = _dbHelper.UpdateApplicationStatus(appId, newStatus);
                            break;
                        case "APPROVE_ASSIGN_ROOM": // "APPROVE_ASSIGN_ROOM|IdЗаявки|НомерКімнати"
                            int appToAssignId = int.Parse(parts[1]);
                            string roomNum = parts[2];
                            response = _dbHelper.ApproveAndAssignRoom(appToAssignId, roomNum);
                            break;
                        case "GET_RESIDENTS": // "GET_RESIDENTS"
                            response = _dbHelper.GetResidents();
                            break;
                        case "SEND_MESSAGE": // "SEND_MESSAGE|TargetLogin|MessageText"
                            response = _dbHelper.SendMessage(parts[1], parts[2]);
                            break;
                        case "GET_MESSAGES": // "GET_MESSAGES|login"
                            response = _dbHelper.GetMessages(parts[1]);
                            break;
                        case "GET_SETTLEMENT_REPORT": // "GET_SETTLEMENT_REPORT"
                            response = _dbHelper.GetSettlementReport();
                            break;
                        case "GET_ROOMS_DATA": // "GET_ROOMS_DATA"
                            response = _dbHelper.GetRoomsData();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    response = $"ERROR|Помилка на сервері: {ex.Message}";
                }

                byte[] responseData = Encoding.UTF8.GetBytes(response);
                await stream.WriteAsync(responseData, 0, responseData.Length);
                Console.WriteLine($"Надіслано відповідь: {response}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Помилка: {e.Message}");
            }
            finally
            {
                client.Close();
            }
        }
    }
}