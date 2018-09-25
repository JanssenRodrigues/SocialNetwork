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
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("CreateProfile", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Email", profile.Email);
            sqlCommand.Parameters.AddWithValue("FirstName", profile.FirstName);
            sqlCommand.Parameters.AddWithValue("LastName", profile.LastName);
            sqlCommand.Parameters.AddWithValue("BirthDay", profile.BirthDay);
            sqlCommand.Parameters.AddWithValue("AccountId", profile.Email);
            sqlCommand.Parameters.AddWithValue("PhotoUrl", profile.PhotoUrl);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            return profile;
        }

        public Profile Get(int? id)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            Profile profile = new Profile();
            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("GetProfileById", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Id", id.ToString());
            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                profile.Id = int.Parse(reader["Id"].ToString());
                profile.FirstName = reader["FirstName"].ToString();
                profile.LastName = reader["LastName"].ToString();
                profile.BirthDay = DateTime.Parse(reader["Birthday"].ToString());
                profile.PhotoUrl = reader["PhotoUrl"].ToString();
                profile.Email = reader["Email"].ToString();
            }

            sqlConnection.Close();
            return profile;
        }

        public Profile GetByEmail(string email)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            Profile profile = new Profile();
            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("GetProfileByEmail", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Email", email);
            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                profile.Id = int.Parse(reader["Id"].ToString());
                profile.FirstName = reader["FirstName"].ToString();
                profile.LastName = reader["LastName"].ToString();
                profile.BirthDay = DateTime.Parse(reader["BirthDay"].ToString());
                profile.PhotoUrl = reader["PhotoUrl"].ToString();
                profile.Email = reader["Email"].ToString();
            }

            sqlConnection.Close();
            return profile;
        }


        public Profile EditProfile(Profile profile)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            //######### OBTEM TODOS OS PROFILES ##########
            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("EditProfile", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Email", profile.Email);
            sqlCommand.Parameters.AddWithValue("FirstName", profile.FirstName);
            sqlCommand.Parameters.AddWithValue("LastName", profile.LastName);
            sqlCommand.Parameters.AddWithValue("BirthDay", profile.BirthDay);
            sqlCommand.Parameters.AddWithValue("PhotoUrl", profile.PhotoUrl);
            sqlCommand.ExecuteReader();

            //############################################

            sqlConnection.Close();
            return profile;
        }

        public Profile DeleteProfile(Profile profile)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("DeleteProfile", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Email", profile.Email);
            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                profile.Id = int.Parse(reader["Id"].ToString());
                profile.Email = reader["Email"].ToString();
            }

            sqlConnection.Close();
            return profile;
        }
    }
}
