using System.Data.SQLite;
using System.IO;

namespace DormitoryServer
{
    public class DatabaseHelper
    {
        private string _connectionString;
        private const string DbName = "dormitory.sqlite";

        public DatabaseHelper()
        {
            _connectionString = $"Data Source={DbName};Version=3;";
            if (!File.Exists(DbName))
            {
                SQLiteConnection.CreateFile(DbName);
                InitializeDatabase();
            }
        }

        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string sql = @"
                    CREATE TABLE Users (
                        Login TEXT PRIMARY KEY,
                        Password TEXT NOT NULL,
                        FullName TEXT,
                        Role TEXT DEFAULT 'Student' 
                    );

                    CREATE TABLE Applications (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserLogin TEXT NOT NULL,
                        FullName TEXT,
                        StudentId TEXT,
                        Reason TEXT,
                        Status TEXT DEFAULT 'В обробці',
                        AssignedRoom TEXT,
                        FOREIGN KEY(UserLogin) REFERENCES Users(Login)
                    );

                    CREATE TABLE Messages (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserLogin TEXT NOT NULL, -- 'Всі' або логін студента
                        MessageText TEXT NOT NULL,
                        Sender TEXT DEFAULT 'Адміністрація'
                    );

                    CREATE TABLE Rooms (
                        RoomNumber TEXT PRIMARY KEY,
                        Capacity INT NOT NULL,
                        Occupied INT DEFAULT 0
                    );";

                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                string adminSql = "INSERT INTO Users (Login, Password, FullName, Role) VALUES ('admin', 'admin', 'Адміністратор', 'Admin');";
                new SQLiteCommand(adminSql, connection).ExecuteNonQuery();

                string roomsSql = @"
                    INSERT OR IGNORE INTO Rooms (RoomNumber, Capacity, Occupied) VALUES
                    ('101A', 4, 2),
                    ('102A', 3, 3), 
                    ('103A', 4, 1),
                    ('201B', 2, 2),
                    ('202B', 4, 0); 
                ";
                new SQLiteCommand(roomsSql, connection).ExecuteNonQuery();

            }
        }


        public string RegisterUser(string login, string password, string fullName)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string checkSql = "SELECT COUNT(1) FROM Users WHERE Login = @login";
                    var checkCmd = new SQLiteCommand(checkSql, connection);
                    checkCmd.Parameters.AddWithValue("@login", login);
                    long count = (long)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        return "ERROR|Користувач з таким логіном вже існує.";
                    }

                    string insertSql = "INSERT INTO Users (Login, Password, FullName, Role) VALUES (@login, @password, @fullName, 'Student')";
                    var insertCmd = new SQLiteCommand(insertSql, connection);
                    insertCmd.Parameters.AddWithValue("@login", login);
                    insertCmd.Parameters.AddWithValue("@password", password); 
                    insertCmd.Parameters.AddWithValue("@fullName", fullName);
                    insertCmd.ExecuteNonQuery();
                    return "OK|Реєстрація успішна.";
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }

        public string ValidateUser(string login, string password)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Password, Role FROM Users WHERE Login = @login";
                    var command = new SQLiteCommand(sql, connection);
                    command.Parameters.AddWithValue("@login", login);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            return "ERROR|Користувача не знайдено.";
                        }
                        if (reader["Password"].ToString() == password)
                        {
                            return $"OK|{reader["Role"].ToString()}";
                        }
                        return "ERROR|Невірний пароль.";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }

        public string SubmitApplication(string login, string fullName, string studentId, string reason)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string checkSql = "SELECT COUNT(1) FROM Applications WHERE UserLogin = @login";
                    var checkCmd = new SQLiteCommand(checkSql, connection);
                    checkCmd.Parameters.AddWithValue("@login", login);
                    if ((long)checkCmd.ExecuteScalar() > 0)
                    {
                        return "ERROR|Ви вже подавали заявку.";
                    }

                    string insertSql = "INSERT INTO Applications (UserLogin, FullName, StudentId, Reason) VALUES (@login, @fullName, @studentId, @reason)";
                    var insertCmd = new SQLiteCommand(insertSql, connection);
                    insertCmd.Parameters.AddWithValue("@login", login);
                    insertCmd.Parameters.AddWithValue("@fullName", fullName);
                    insertCmd.Parameters.AddWithValue("@studentId", studentId);
                    insertCmd.Parameters.AddWithValue("@reason", reason);
                    insertCmd.ExecuteNonQuery();

                    return "OK|Заявку успішно подано!";
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }

        public string GetApplicationStatus(string login)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Status FROM Applications WHERE UserLogin = @login";
                    var command = new SQLiteCommand(sql, connection);
                    command.Parameters.AddWithValue("@login", login);

                    var result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return $"OK|{result.ToString()}";
                    }

                    return "ERROR|Заявку не знайдено.";
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }

        public string GetApplicationData(string login)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT FullName, StudentId, Reason FROM Applications WHERE UserLogin = @login";
                    var command = new SQLiteCommand(sql, connection);
                    command.Parameters.AddWithValue("@login", login);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return $"OK|{reader["FullName"]}|{reader["StudentId"]}|{reader["Reason"]}";
                        }
                        return "ERROR|Заявку не знайдено.";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }

        public string UpdateApplication(string login, string fullName, string studentId, string reason)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string sql = @"
                        UPDATE Applications 
                        SET FullName = @fullName, StudentId = @studentId, Reason = @reason, Status = 'В обробці'
                        WHERE UserLogin = @login";

                    var command = new SQLiteCommand(sql, connection);
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@fullName", fullName);
                    command.Parameters.AddWithValue("@studentId", studentId);
                    command.Parameters.AddWithValue("@reason", reason);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return "OK|Зміни успішно збережено.";
                    }
                    return "ERROR|Не вдалося оновити заявку.";
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }
        public string GetAllApplications()
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Id, FullName, StudentId, Status, Reason, UserLogin FROM Applications WHERE Status = 'В обробці'";

                    var command = new SQLiteCommand(sql, connection);

                    string response = "OK";
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return "EMPTY|Немає заявок, що очікують обробки.";
                        }

                        while (reader.Read())
                        {
                            response += $"|{reader["Id"]};{reader["FullName"]};{reader["StudentId"]};{reader["Status"]};{reader["Reason"]};{reader["UserLogin"]}";
                        }
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }

        public string UpdateApplicationStatus(int applicationId, string newStatus)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Applications SET Status = @status WHERE Id = @id";
                    var command = new SQLiteCommand(sql, connection);
                    command.Parameters.AddWithValue("@status", newStatus);
                    command.Parameters.AddWithValue("@id", applicationId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return "OK|Статус оновлено.";
                    }
                    return "ERROR|Не вдалося оновити статус.";
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }
        public string GetResidents()
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string sql = @"
                    SELECT a.FullName, u.Login, a.StudentId, a.AssignedRoom -- <--- ДОДАЙТЕ a.AssignedRoom
                    FROM Applications a
                    JOIN Users u ON a.UserLogin = u.Login
                    WHERE a.Status LIKE 'Поселено%'";

                    var command = new SQLiteCommand(sql, connection);

                    string response = "OK";
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return "EMPTY|Список мешканців порожній.";
                        }

                        while (reader.Read())
                        {
                            response += $"|{reader["FullName"]};{reader["Login"]};{reader["StudentId"]};{reader["AssignedRoom"]}";
                        }
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }
        public string SendMessage(string targetLogin, string messageText)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Messages (UserLogin, MessageText) VALUES (@login, @message)";
                    var command = new SQLiteCommand(sql, connection);
                    command.Parameters.AddWithValue("@login", targetLogin); 
                    command.Parameters.AddWithValue("@message", messageText);
                    command.ExecuteNonQuery();
                    return "OK|Повідомлення надіслано.";
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }

        public string GetMessages(string login)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Sender, MessageText FROM Messages WHERE UserLogin = @login OR UserLogin = 'Всі'";
                    var command = new SQLiteCommand(sql, connection);
                    command.Parameters.AddWithValue("@login", login);

                    string response = "OK";
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return "EMPTY|У вас немає повідомлень.";
                        }
                        while (reader.Read())
                        {
                            response += $"|{reader["Sender"]}:{reader["MessageText"]}";
                        }
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }
        public string GetSettlementReport()
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT Status, COUNT(*) as Count FROM Applications GROUP BY Status";
                    var command = new SQLiteCommand(sql, connection);

                    int pending = 0;
                    int approved = 0;
                    int rejected = 0;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string status = reader["Status"].ToString();
                            int count = Convert.ToInt32(reader["Count"]);

                            if (status == "В обробці") pending = count;
                            else if (status == "Підтверджено") approved = count;
                            else if (status == "Відхилено") rejected = count;
                        }
                    }

                    string report = "--- Звіт по поселенню ---\n\n";
                    report += $"Заявок в обробці: {pending}\n";
                    report += $"Заявок підтверджено (мешканців): {approved}\n";
                    report += $"Заявок відхилено: {rejected}\n";
                    report += $"----------------------------------\n";
                    report += $"Всього заявок: {pending + approved + rejected}";

                    return $"OK|{report}";
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }
        public string GetRoomsData()
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    string sql = "SELECT RoomNumber, Capacity, Occupied FROM Rooms";
                    var command = new SQLiteCommand(sql, connection);

                    string response = "OK";
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return "EMPTY|Дані про кімнати відсутні.";
                        }

                        while (reader.Read())
                        {
                            response += $"|{reader["RoomNumber"]};{reader["Capacity"]};{reader["Occupied"]}";
                        }
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }
        public string ApproveAndAssignRoom(int applicationId, string roomNumber)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string checkSql = "SELECT Capacity, Occupied FROM Rooms WHERE RoomNumber = @room";
                        var checkCmd = new SQLiteCommand(checkSql, connection);
                        checkCmd.Parameters.AddWithValue("@room", roomNumber);

                        using (var reader = checkCmd.ExecuteReader())
                        {
                            if (!reader.Read()) return "ERROR|Кімнату не знайдено.";

                            int capacity = Convert.ToInt32(reader["Capacity"]);
                            int occupied = Convert.ToInt32(reader["Occupied"]);

                            if (occupied >= capacity)
                            {
                                return "ERROR|Кімната " + roomNumber + " вже заповнена.";
                            }
                        }

                        string updateAppSql = "UPDATE Applications SET Status = 'Поселено - ' || @room, AssignedRoom = @room WHERE Id = @id";
                        var updateAppCmd = new SQLiteCommand(updateAppSql, connection);
                        updateAppCmd.Parameters.AddWithValue("@room", roomNumber);
                        updateAppCmd.Parameters.AddWithValue("@id", applicationId);
                        updateAppCmd.ExecuteNonQuery();

                        string updateRoomSql = "UPDATE Rooms SET Occupied = Occupied + 1 WHERE RoomNumber = @room";
                        var updateRoomCmd = new SQLiteCommand(updateRoomSql, connection);
                        updateRoomCmd.Parameters.AddWithValue("@room", roomNumber);
                        updateRoomCmd.ExecuteNonQuery();

                        transaction.Commit();
                        return "OK|Студента поселено в кімнату " + roomNumber;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); 
                        return $"ERROR|{ex.Message}";
                    }
                }
            }
        }

    }
}