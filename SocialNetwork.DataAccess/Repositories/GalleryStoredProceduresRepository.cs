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
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("CreateGallery", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Name", gallery.Name);
            sqlCommand.Parameters.AddWithValue("ProfileId", gallery.ProfileId);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            return gallery;
        }

        public Gallery GetGallery(int id)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("GetGallery", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Id", id);
            var reader = sqlCommand.ExecuteReader();

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
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("GetGalleriesByProfileId", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("ProfileId", profile.Id);
            var reader = sqlCommand.ExecuteReader();
            
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

        public Photo AddPhoto(Photo photo)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("CreatePhoto", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Url", photo.Url);
            sqlCommand.Parameters.AddWithValue("GalleryId", photo.GalleryId);
            sqlCommand.ExecuteReader();

            sqlConnection.Close();
            return photo;
        }

        public Gallery GetPhotos(Gallery gallery)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("GetPhotosByGalleryId", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("GalleryId", gallery.Id);
            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Photo photo = new Photo();
                photo.Id = int.Parse(reader["Id"].ToString());
                photo.Url = reader["Url"].ToString();
                photo.GalleryId = int.Parse(reader["GalleryId"].ToString());
                gallery.Photos.Add(photo);
            }

            sqlConnection.Close();
            return gallery;
        }


        public Gallery Delete(Gallery gallery)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("DeleteGallery", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Id", gallery.Id);
            var reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                gallery.Id = int.Parse(reader["Id"].ToString());
            }

            sqlConnection.Close();
            return gallery;
        }

        public Gallery Edit(Gallery gallery)
        {
            SqlConnection sqlConnection;
            sqlConnection = new SqlConnection(Properties.Settings.Default.DbConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("EditGallery", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("Id", gallery.Id);
            sqlCommand.Parameters.AddWithValue("Name", gallery.Name);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            return gallery;
        }
    }
}
