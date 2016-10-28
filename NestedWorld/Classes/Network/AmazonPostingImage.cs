using Amazon;
using Amazon.EC2;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NestedWorld.Classes.Network
{
    public class AmazonPostingImage
    {
        private const string AccesKey = "AKIAINX4X7KH4C5TVJBA";
        private string bucketName = "nestedworld";
        private AmazonS3Config config = new AmazonS3Config();
        private IAmazonS3 client;


        public AmazonPostingImage()
        {
           
        }

        public static void SimpleCallback(IAsyncResult asyncResult)
        {
            Utils.Log.Info("Finished PutObject operation with simple callback");
        }

#pragma warning disable CS0618
        public async Task<string> PostImage(StorageFile file, string folder)
        {
            try
            {
                string SecretAccessKey = await Config.Config.GetKey("AWS");
                using (client = new AmazonS3Client(AccesKey, SecretAccessKey, Amazon.RegionEndpoint.USEast1))
                {
                    PutObjectRequest request = new PutObjectRequest()
                    {
                        BucketName = this.bucketName,
                        Key = folder + "/" + file.Name,
                        StorageFile = file
                    };
                    request.Headers["x-amz-acl"] = "public-read";
                    PutObjectResponse response = await client.PutObjectAsync(request);

                    return "https://s3-eu-west-1.amazonaws.com/" + request.BucketName + "/" + request.Key;
                }
            }
            catch (System.Exception ex)
            {
                Utils.Log.Info("AWS", ex);
                return null;
            }
        }
#pragma warning restore CS0618

      
    }
}
