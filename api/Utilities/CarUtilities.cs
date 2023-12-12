using api.Models;
using MySql.Data.MySqlClient;
namespace api.Utilities
{
    public class CarUtilities
    {
        public List<Cars> GetAllCars()
        {
            ConnectionString myConnection = new ConnectionString();
            string cs  = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();
            string stm = "SELECT * FROM cartable ORDER BY CarID ASC";
            using var cmd = new MySqlCommand(stm, con);
            // cmd.Prepare();
            MySqlDataReader rdr = cmd.ExecuteReader();
            List<Cars> Allcars = new List<Cars>();
            while(rdr.Read())
            {
                Allcars.Add(new Cars(){CarID = rdr.GetInt32(0), Make = rdr.GetString(1), Model = rdr.GetString(2), Mileage = rdr.GetInt32(3), Date = rdr.GetString(4), Hold = rdr.GetBoolean(5), Deleted = rdr.GetBoolean(6)});
            }
            con.Close();
            return Allcars;
        }


        public void CreateCar(Cars myCars)
        {
            ConnectionString myConnection = new ConnectionString();
            string cs  = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();
            Console.WriteLine("New Car");

            string stm = @"INSERT INTO cartable(Make, Model, Mileage, Date, Hold, Deleted) VALUES(@Make, @Model, @Mileage, @Date, @Hold, @Deleted)";

            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@Make", myCars.Make);
            cmd.Parameters.AddWithValue("@Model", myCars.Model);
            cmd.Parameters.AddWithValue("@Mileage", myCars.Mileage);
            cmd.Parameters.AddWithValue("@Date", myCars.Date);
            cmd.Parameters.AddWithValue("@Hold", myCars.Hold);
            cmd.Parameters.AddWithValue("@Deleted", myCars.Deleted);
           

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public void UpdateCars(Cars value)
        {
            ConnectionString myConnection = new ConnectionString();
            string cs  = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();
            string stm = @"UPDATE cartable SET Make = @Make, Model = @Model, Mileage = @Mileage, Date = @Date, Hold = @Hold, Deleted = @Deleted WHERE CarID = @CarID";
            MySqlCommand cmd = new MySqlCommand(stm, con);
            
            cmd.Parameters.AddWithValue("@CarID", value.CarID);
            cmd.Parameters.AddWithValue("@Make", value.Make);
            cmd.Parameters.AddWithValue("@Model", value.Model);
            cmd.Parameters.AddWithValue("@Mileage", value.Mileage);
            cmd.Parameters.AddWithValue("@Date", value.Date);
            cmd.Parameters.AddWithValue("@Hold", value.Hold);
            cmd.Parameters.AddWithValue("@Deleted", value.Deleted);
            
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteCar(Cars value)
        {
            ConnectionString myConnection = new ConnectionString();
            string cs = myConnection.cs;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"UPDATE cartable SET Deleted = 1 WHERE CarID = @id";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@id", value.CarID);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

    }
}