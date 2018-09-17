using SocialNetwork.Core.Interfaces.Repositories;
using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Repositories
{
    public class ProfileStoredProcedureRepository : IProfileRepository
    {
        public Profile Create(Profile profile)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Janssen\source\repos\SocialNetwork\SocialNetwork.Api\App_Data\aspnet-SocialNetwork.Api-20180812114050.mdf;Initial Catalog=aspnet-SocialNetwork.Api-20180812114050;Integrated Security=True");
            //sqlConnection = new SqlConnection("Server=tcp:myinfnetsocialnetwork.database.windows.net,1433;Initial Catalog=MySocialNetwork;Persist Security Info=False;User ID=olivato;Password=EDSInf123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            sqlConnection.Open();

            //######### INSERE NOVO PROFILE ##########
            SqlCommand sqlCommandAddProfile;
            sqlCommandAddProfile = new SqlCommand("CreateProfile", sqlConnection);
            sqlCommandAddProfile.CommandType = System.Data.CommandType.StoredProcedure;
            //sqlCommandAddProfile.Parameters.AddWithValue("Id", profile.Id);
            sqlCommandAddProfile.Parameters.AddWithValue("Email", profile.Email);
            sqlCommandAddProfile.Parameters.AddWithValue("FirstName", profile.FirstName);
            sqlCommandAddProfile.Parameters.AddWithValue("LastName", profile.LastName);
            sqlCommandAddProfile.Parameters.AddWithValue("BirthDay", profile.BirthDay);
            sqlCommandAddProfile.Parameters.AddWithValue("AccountId", profile.Email);
            //sqlCommandAddProfile.Parameters.AddWithValue("Photo", profile.Photo);
            sqlCommandAddProfile.ExecuteNonQuery();
            //########################################

            sqlConnection.Close();
            return profile;
        }

        public Profile Get(int? id)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Janssen\source\repos\SocialNetwork\SocialNetwork.Api\App_Data\aspnet-SocialNetwork.Api-20180812114050.mdf;Initial Catalog=aspnet-SocialNetwork.Api-20180812114050;Integrated Security=True");
            sqlConnection.Open();

            Profile profile = new Profile();
            //######### OBTEM TODOS OS PROFILES ##########
            SqlCommand sqlCommandGetProfile;
            sqlCommandGetProfile = new SqlCommand("GetProfile", sqlConnection);
            sqlCommandGetProfile.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommandGetProfile.Parameters.AddWithValue("Id", id.ToString());
            var reader = sqlCommandGetProfile.ExecuteReader();

            while (reader.Read())
            {
                profile.Id = int.Parse(reader["Id"].ToString());
                profile.FirstName = reader["FirstName"].ToString();
                profile.LastName = reader["LastName"].ToString();
                profile.BirthDay = DateTime.Parse(reader["Birthday"].ToString());
                //profile.Photo = reader["Photo"].ToString();
                profile.Email = reader["Email"].ToString();
            }
            //############################################

            sqlConnection.Close();
            return profile;
        }

        public Profile GetByEmail(string email)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Janssen\source\repos\SocialNetwork\SocialNetwork.Api\App_Data\aspnet-SocialNetwork.Api-20180812114050.mdf;Initial Catalog=aspnet-SocialNetwork.Api-20180812114050;Integrated Security=True");
            sqlConnection.Open();

            Profile profile = new Profile();
            //######### OBTEM TODOS OS PROFILES ##########
            SqlCommand sqlCommandGetProfile;
            sqlCommandGetProfile = new SqlCommand("GetProfileByEmail", sqlConnection);
            sqlCommandGetProfile.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommandGetProfile.Parameters.AddWithValue("Email", email);
            var reader = sqlCommandGetProfile.ExecuteReader();

            while (reader.Read())
            {
                profile.Id = int.Parse(reader["Id"].ToString());
                profile.FirstName = reader["FirstName"].ToString();
                profile.LastName = reader["LastName"].ToString();
                profile.BirthDay = DateTime.Parse(reader["BirthDay"].ToString());
                //profile.Photo = reader["Photo"].ToString();
                profile.Email = reader["Email"].ToString();
            }
            //############################################

            sqlConnection.Close();
            return profile;
        }


        public Profile EditProfile(Profile profile)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Janssen\source\repos\SocialNetwork\SocialNetwork.Api\App_Data\aspnet-SocialNetwork.Api-20180812114050.mdf;Initial Catalog=aspnet-SocialNetwork.Api-20180812114050;Integrated Security=True");
            sqlConnection.Open();

            //######### OBTEM TODOS OS PROFILES ##########
            SqlCommand sqlCommandGetProfile;
            sqlCommandGetProfile = new SqlCommand("EditProfile", sqlConnection);
            sqlCommandGetProfile.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommandGetProfile.Parameters.AddWithValue("Email", profile.Email);
            sqlCommandGetProfile.Parameters.AddWithValue("FirstName", profile.FirstName);
            sqlCommandGetProfile.Parameters.AddWithValue("LastName", profile.LastName);
            sqlCommandGetProfile.Parameters.AddWithValue("BirthDay", profile.BirthDay);
            var reader = sqlCommandGetProfile.ExecuteReader();

            while (reader.Read())
            {
                profile.Id = int.Parse(reader["Id"].ToString());
                profile.Email = reader["Email"].ToString();
                profile.FirstName = reader["FirstName"].ToString();
                profile.LastName = reader["LastName"].ToString();
                profile.BirthDay = DateTime.Parse(reader["BirthDay"].ToString());
            }
            //############################################

            sqlConnection.Close();
            return profile;
        }

        public Profile DeleteProfile(Profile profile)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Janssen\source\repos\SocialNetwork\SocialNetwork.Api\App_Data\aspnet-SocialNetwork.Api-20180812114050.mdf;Initial Catalog=aspnet-SocialNetwork.Api-20180812114050;Integrated Security=True");
            sqlConnection.Open();

            //######### OBTEM TODOS OS PROFILES ##########
            SqlCommand sqlCommandGetProfile;
            sqlCommandGetProfile = new SqlCommand("DeleteProfile", sqlConnection);
            sqlCommandGetProfile.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommandGetProfile.Parameters.AddWithValue("Email", profile.Email);
            var reader = sqlCommandGetProfile.ExecuteReader();

            while (reader.Read())
            {
                profile.Id = int.Parse(reader["Id"].ToString());
                profile.Email = reader["Email"].ToString();
            }
            //############################################

            sqlConnection.Close();
            return profile;
        }
    }
}
