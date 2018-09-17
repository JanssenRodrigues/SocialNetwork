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

            //######### VERIFICA SE UM PERFIL JA SEGUE OUTRO PERFIL ##########
            SqlCommand sqlCommandAddProfile;
            sqlCommandAddProfile = new SqlCommand("CheckFollow", sqlConnection);
            sqlCommandAddProfile.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommandAddProfile.Parameters.AddWithValue("UserId", UserId);
            sqlCommandAddProfile.Parameters.AddWithValue("FollowingId", FollowingId);
            sqlCommandAddProfile.ExecuteNonQuery();
            //########################################

            sqlConnection.Close();
            return follow;
        }
    }
}
