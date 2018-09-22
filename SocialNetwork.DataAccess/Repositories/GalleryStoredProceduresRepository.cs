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
    public class GalleryStoredProcedureRepository : IGalleryRepository
    {
        

        public Gallery Create(Gallery gallery)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Janssen\source\repos\SocialNetwork\SocialNetwork.Api\App_Data\aspnet-SocialNetwork.Api-20180812114050.mdf;Initial Catalog=aspnet-SocialNetwork.Api-20180812114050;Integrated Security=True");
            //sqlConnection = new SqlConnection("Server=tcp:myinfnetsocialnetwork.database.windows.net,1433;Initial Catalog=MySocialNetwork;Persist Security Info=False;User ID=olivato;Password=EDSInf123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            sqlConnection.Open();

            //######### INSERE NOVO PROFILE ##########
            SqlCommand sqlCommandAddProfile;
            sqlCommandAddProfile = new SqlCommand("CreateGallery", sqlConnection);
            sqlCommandAddProfile.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommandAddProfile.Parameters.AddWithValue("Name", gallery.Name);
            sqlCommandAddProfile.Parameters.AddWithValue("ProfileId", gallery.ProfileId);
            sqlCommandAddProfile.ExecuteNonQuery();
            //########################################

            sqlConnection.Close();
            return gallery;
        }

        public Gallery GetGallery(int id)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Janssen\source\repos\SocialNetwork\SocialNetwork.Api\App_Data\aspnet-SocialNetwork.Api-20180812114050.mdf;Initial Catalog=aspnet-SocialNetwork.Api-20180812114050;Integrated Security=True");
            //sqlConnection = new SqlConnection("Server=tcp:myinfnetsocialnetwork.database.windows.net,1433;Initial Catalog=MySocialNetwork;Persist Security Info=False;User ID=olivato;Password=EDSInf123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            sqlConnection.Open();
            //######### INSERE NOVO PROFILE ##########
            SqlCommand sqlCommandAddProfile;
            sqlCommandAddProfile = new SqlCommand("GetGallery", sqlConnection);
            sqlCommandAddProfile.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommandAddProfile.Parameters.AddWithValue("Id", id);
            var reader = sqlCommandAddProfile.ExecuteReader();
            //########################################

            Gallery gallery = new Gallery();
            while (reader.Read())
            {
                gallery.Id = int.Parse(reader["Id"].ToString());
                gallery.Name = reader["Name"].ToString();
                gallery.ProfileId = int.Parse(reader["ProfileId"].ToString());
            }

            sqlConnection.Close();
            return gallery;
        }

        public Profile GetGalleriesByProfileId(Profile profile)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Janssen\source\repos\SocialNetwork\SocialNetwork.Api\App_Data\aspnet-SocialNetwork.Api-20180812114050.mdf;Initial Catalog=aspnet-SocialNetwork.Api-20180812114050;Integrated Security=True");
            //sqlConnection = new SqlConnection("Server=tcp:myinfnetsocialnetwork.database.windows.net,1433;Initial Catalog=MySocialNetwork;Persist Security Info=False;User ID=olivato;Password=EDSInf123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            sqlConnection.Open();
            //######### INSERE NOVO PROFILE ##########
            SqlCommand sqlCommandAddProfile;
            sqlCommandAddProfile = new SqlCommand("GetGalleriesByProfileId", sqlConnection);
            sqlCommandAddProfile.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommandAddProfile.Parameters.AddWithValue("ProfileId", profile.Id);
            var reader = sqlCommandAddProfile.ExecuteReader();
            //########################################


            while (reader.Read())
            {
                Gallery gallery = new Gallery();
                gallery.Id = int.Parse(reader["Id"].ToString());
                gallery.Name = reader["Name"].ToString();
                gallery.ProfileId = int.Parse(reader["ProfileId"].ToString());
                profile.Galleries.Add(gallery);
            }

            sqlConnection.Close();
            return profile;
        }

        public Gallery AddPhoto(Photo photo)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Janssen\source\repos\SocialNetwork\SocialNetwork.Api\App_Data\aspnet-SocialNetwork.Api-20180812114050.mdf;Initial Catalog=aspnet-SocialNetwork.Api-20180812114050;Integrated Security=True");
            //sqlConnection = new SqlConnection("Server=tcp:myinfnetsocialnetwork.database.windows.net,1433;Initial Catalog=MySocialNetwork;Persist Security Info=False;User ID=olivato;Password=EDSInf123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            sqlConnection.Open();
            //######### INSERE NOVO PROFILE ##########
            SqlCommand sqlCommandAddProfile;
            sqlCommandAddProfile = new SqlCommand("CreatePhoto", sqlConnection);
            sqlCommandAddProfile.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommandAddProfile.Parameters.AddWithValue("PhotoUrl", photo.Url);
            sqlCommandAddProfile.Parameters.AddWithValue("GalleryId", photo.GalleryId);
            var reader = sqlCommandAddProfile.ExecuteReader();
            //########################################


            Gallery gallery = new Gallery();
            while (reader.Read())
            {
                gallery.Id = int.Parse(reader["Id"].ToString());
                gallery.Name = reader["Name"].ToString();
                gallery.ProfileId = int.Parse(reader["ProfileId"].ToString());
                //gallery.Photos.Add(photo);
            }

            sqlConnection.Close();
            return gallery;
        }

    }
}
