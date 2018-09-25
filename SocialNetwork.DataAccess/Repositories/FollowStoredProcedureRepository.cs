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
    public class FollowStoredProcedureRepository : IFollowRepository
    {
        public Follow FollowProfile(Follow follow)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("FollowProfile", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("UserId", follow.UserId);
            sqlCommand.Parameters.AddWithValue("FollowingId", follow.FollowingId);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            return follow;
        }

        public Follow CheckFollow(int UserId, int FollowingId)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            Follow follow = new Follow();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("CheckFollow", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("UserId", UserId);
            sqlCommand.Parameters.AddWithValue("FollowingId", FollowingId);
            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                follow.Id = int.Parse(reader["Id"].ToString());
                follow.UserId = int.Parse(reader["UserId"].ToString());
                follow.FollowingId = int.Parse(reader["FollowingId"].ToString());
            }

            sqlConnection.Close();
            return follow;
        }

        public Follow DeleteFollow(int UserId, int FollowingId)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            Follow follow = new Follow();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("DeleteFollow", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("UserId", UserId);
            sqlCommand.Parameters.AddWithValue("FollowingId", FollowingId);
            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                follow.UserId = int.Parse(reader["UserId"].ToString());
                follow.FollowingId = int.Parse(reader["FollowingId"].ToString());
            }

            sqlConnection.Close();
            return follow;
        }
    }
}
