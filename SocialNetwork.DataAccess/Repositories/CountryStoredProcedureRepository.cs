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
    public class CountryStoredProcedureRepository : ICountryRepository
    {
        public Country Create(Country country)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("CreateCountry", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Name", country.Name);
            sqlCommand.Parameters.AddWithValue("PhotoUrl", country.PhotoUrl);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            return country;
        }

        public Country Edit(Country country)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("EditCountry", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Id", country.Id);
            sqlCommand.Parameters.AddWithValue("Name", country.Name);
            sqlCommand.Parameters.AddWithValue("PhotoUrl", country.PhotoUrl);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            return country;
        }

        public Country Delete(Country country)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("DeleteCountry", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Id", country.Id);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            return country;
        }

        public IEnumerable<Country> GetAll()
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("GetAllCountries", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            var reader = sqlCommand.ExecuteReader();

            List<Country> countries = new List<Country>();
            while (reader.Read())
            {
                Country country = new Country();
                country.Id = int.Parse(reader["Id"].ToString());
                country.Name = reader["Name"].ToString();
                country.PhotoUrl = reader["PhotoUrl"].ToString();
                countries.Add(country);
            }

            sqlConnection.Close();
            return countries;
        }

        public Country Get(int? id)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("GetCountry", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Id", id);
            var reader = sqlCommand.ExecuteReader();

            Country country = new Country();
            while (reader.Read())
            {
                country.Id = int.Parse(reader["Id"].ToString());
                country.Name = reader["Name"].ToString();
                country.PhotoUrl = reader["PhotoUrl"].ToString();
            }

            sqlConnection.Close();
            return country;
        }
    }
}
