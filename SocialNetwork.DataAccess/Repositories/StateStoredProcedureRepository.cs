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
    public class StateStoredProcedureRepository : IStateRepository
    {
        public State Create(State state)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("CreateState", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Name", state.Name);
            sqlCommand.Parameters.AddWithValue("PhotoUrl", state.PhotoUrl);
            sqlCommand.Parameters.AddWithValue("CountryId", state.CountryId);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            return state;
        }

        public State Delete(State state)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("DeleteState", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Id", state.Id);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            return state;
        }

        public State Edit(State state)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("EditState", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Id", state.Id);
            sqlCommand.Parameters.AddWithValue("Name", state.Name);
            sqlCommand.Parameters.AddWithValue("PhotoUrl", state.PhotoUrl);
            sqlCommand.Parameters.AddWithValue("CountryId", state.CountryId);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            return state;
        }

        public State Get(int? id)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("GetState", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Id", id);
            var reader = sqlCommand.ExecuteReader();

            State state = new State();
            while (reader.Read())
            {
                state.Id = int.Parse(reader["Id"].ToString());
                state.Name = reader["Name"].ToString();
                state.PhotoUrl = reader["PhotoUrl"].ToString();
                state.CountryId = int.Parse(reader["CountryId"].ToString());
            }
            sqlConnection.Close();
            return state;
        }

        public IEnumerable<State> GetAll()
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("GetAllStates", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            var reader = sqlCommand.ExecuteReader();

            List<State> states = new List<State>();
            while (reader.Read())
            {
                State state = new State();
                state.Id = int.Parse(reader["Id"].ToString());
                state.Name = reader["Name"].ToString();
                state.PhotoUrl = reader["PhotoUrl"].ToString();
                state.CountryId = int.Parse(reader["CountryId"].ToString());
                states.Add(state);
            }

            sqlConnection.Close();
            return states;
        }

        //public Country GetCountryById(int? id)
        //{
        //    SqlConnection sqlConnection;
        //    sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
        //    sqlConnection.Open();

        //    SqlCommand sqlCommand;
        //    sqlCommand = new SqlCommand("GetCountry", sqlConnection);
        //    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //    sqlCommand.Parameters.AddWithValue("Id", id);
        //    var reader = sqlCommand.ExecuteReader();

        //    Country country = new Country();
        //    while (reader.Read())
        //    {
        //        country.Name = reader["Name"].ToString();
        //    }

        //    sqlConnection.Close();
        //    return country;
        //}
    }
}
