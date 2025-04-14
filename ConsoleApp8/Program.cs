using System;
using Microsoft.Data.SqlClient;

class Program
{
    static string connectionString = "Server=localhost;Database=Users;Trusted_Connection=True;";

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Sign In");
            Console.WriteLine("2. Sign Up");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SignIn();
                    break;
                case "2":
                    SignUp();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice! Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void SignUp()
    {
        Console.Write("First Name: ");
        string firstName = Console.ReadLine();
        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();
        Console.Write("Age: ");
        int age = int.Parse(Console.ReadLine());
        Console.Write("Login: ");
        string login = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Users (FirstName, LastName, Age, Login, Password) " +
                           "VALUES (@FirstName, @LastName, @Age, @Login, @Password)";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Age", age);
            cmd.Parameters.AddWithValue("@Login", login);
            cmd.Parameters.AddWithValue("@Password", password);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Registration successful!");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }

    static void SignIn()
    {
        Console.Write("Login: ");
        string login = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Login = @Login AND Password = @Password";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Login", login);
            cmd.Parameters.AddWithValue("@Password", password);

            conn.Open();
            int count = (int)cmd.ExecuteScalar();

            if (count > 0)
            {
                Console.WriteLine("Login successful!");
            }
            else
            {
                Console.WriteLine("Incorrect login or password.");
            }
        }
    }
}
