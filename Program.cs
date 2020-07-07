using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FileUpload
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
            //Console.WriteLine("Hello World!");

        }        
        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                //UriBuilder uriBuilder = new UriBuilder("http://api.qrserver.com/v1/read-qr-code/");
                client.BaseAddress = new Uri("http://api.qrserver.com/v1/read-qr-code/");
                //new Uri(uriBuilder.ToString());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string filepath = "C:/Users/neha1/Desktop/test.PNG";
                string filename = "test.PNG";
                //using(var formData= new MultipartFormDataContent)
                //{
                //    formData.Add(stringContent)
                //}

                MultipartFormDataContent content = new MultipartFormDataContent();
                //HttpContent param=new StringContent("")
                //content.Add()

                ByteArrayContent fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filepath));
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = filename };
                content.Add(fileContent);

                HttpResponseMessage response = await client.PostAsync("http://api.qrserver.com/v1/read-qr-code/?fileUrl="+filename, content);
                string returnString = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
