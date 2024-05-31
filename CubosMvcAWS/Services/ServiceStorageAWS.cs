using Amazon.S3.Model;
using Amazon.S3;
using CubosMvcAWS.Models;

namespace CubosMvcAWS.Services
{
    public class ServiceStorageAWS
    {
        private IAmazonS3 client;
        private string BucketName;

        public ServiceStorageAWS
            (KeysModel keysModel, IAmazonS3 client)
        {
            this.BucketName = keysModel.BucketName;
            this.client = client;
        }

        //METODO PARA SUBIR LAS IMAGENES DONDE NECESITAMOS
        //EL NOMBRE DE LA IMAGEN, EL STREAM, EL BUCKET NAME
        public async Task<bool>
            UploadFileAsync(string fileName, Stream stream)
        {
            PutObjectRequest request = new PutObjectRequest
            {
                InputStream = stream,
                Key = fileName,
                BucketName = this.BucketName
            };
            PutObjectResponse response =
                await this.client.PutObjectAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
