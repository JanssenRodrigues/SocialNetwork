﻿using SocialNetwork.Core.Interfaces.Repositories;
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
            sqlConnection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Janssen\source\repos\SocialNetwork\SocialNetwork.Api\App_Data\aspnet-SocialNetwork.Api-20180812114050.mdf;Initial Catalog=aspnet-SocialNetwork.Api-20180812114050;Integrated Security=True");
            //sqlConnection = new SqlConnection("Server=tcp:myinfnetsocialnetwork.database.windows.net,1433;Initial Catalog=MySocialNetwork;Persist Security Info=False;User ID=olivato;Password=EDSInf123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            sqlConnection.Open();

            //######### INSERE NOVO SEGUIDOR ##########
            SqlCommand sqlCommandAddProfile;
            sqlCommandAddProfile = new SqlCommand("FollowProfile", sqlConnection);
            sqlCommandAddProfile.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommandAddProfile.Parameters.AddWithValue("UserId", follow.UserId);
            sqlCommandAddProfile.Parameters.AddWithValue("FollowingId", follow.FollowingId);
            sqlCommandAddProfile.ExecuteNonQuery();
            //########################################

            sqlConnection.Close();
            return follow;
        }

        public Follow CheckFollow(int UserId, int FollowingId)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Janssen\source\repos\SocialNetwork\SocialNetwork.Api\App_Data\aspnet-SocialNetwork.Api-20180812114050.mdf;Initial Catalog=aspnet-SocialNetwork.Api-20180812114050;Integrated Security=True");
            //sqlConnection = new SqlConnection("Server=tcp:myinfnetsocialnetwork.database.windows.net,1433;Initial Catalog=MySocialNetwork;Persist Security Info=False;User ID=olivato;Password=EDSInf123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            sqlConnection.Open();

            Follow follow = new Follow();
            //######### VERIFICA SE UM PERFIL JA SEGUE OUTRO PERFIL ##########
            SqlCommand sqlCommandAddProfile;
            sqlCommandAddProfile = new SqlCommand("CheckFollow", sqlConnection);
            sqlCommandAddProfile.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommandAddProfile.Parameters.AddWithValue("UserId", UserId);
            sqlCommandAddProfile.Parameters.AddWithValue("FollowingId", FollowingId);
            var reader = sqlCommandAddProfile.ExecuteReader();

            while (reader.Read())
            {
                follow.Id = int.Parse(reader["Id"].ToString());
                follow.UserId = int.Parse(reader["UserId"].ToString());
                follow.FollowingId = int.Parse(reader["FollowingId"].ToString());
            }
            //########################################

            sqlConnection.Close();
            return follow;
        }

        public Follow DeleteFollow(int UserId, int FollowingId)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Janssen\source\repos\SocialNetwork\SocialNetwork.Api\App_Data\aspnet-SocialNetwork.Api-20180812114050.mdf;Initial Catalog=aspnet-SocialNetwork.Api-20180812114050;Integrated Security=True");
            sqlConnection.Open();

            Follow follow = new Follow();
            //######### OBTEM TODOS OS PROFILES ##########
            SqlCommand sqlCommandGetProfile;
            sqlCommandGetProfile = new SqlCommand("DeleteFollow", sqlConnection);
            sqlCommandGetProfile.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommandGetProfile.Parameters.AddWithValue("UserId", UserId);
            sqlCommandGetProfile.Parameters.AddWithValue("FollowingId", FollowingId);
            var reader = sqlCommandGetProfile.ExecuteReader();

            while (reader.Read())
            {
                follow.UserId = int.Parse(reader["UserId"].ToString());
                follow.FollowingId = int.Parse(reader["FollowingId"].ToString());
            }
            //############################################

            sqlConnection.Close();
            return follow;
        }
    }
}