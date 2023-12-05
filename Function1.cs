using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UpdateQuizBowlOnBlobUpdate.Models;

namespace UpdateQuizBowlOnBlobUpdate
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }


        [Function(nameof(Function1))]
        public async Task Run([BlobTrigger("quizbowlcontainer/{name}.txt", Connection = "OrdinaryGeeksContainer")] string blobTrigger, string name,
        [BlobInput("quizbowlcontainer/{name}.txt", Connection = "OrdinaryGeeksContainer")] string blobContent)
        {
            HttpClient httpClient = new HttpClient();


            string[] entries = blobContent.Split(Environment.NewLine);

            Question[] questions = new Question[entries.Length];
            int index = 0;
            foreach (string entry in entries)
            {
                string[] questionStringArray = entry.Split('.');
                questions[index] = new Question();
                questions[index].Text = questionStringArray[0];
                questions[index].Answer = questionStringArray[1];
                questions[index].Points = int.Parse(questionStringArray[2]);
                questions[index].Category = questionStringArray[3];

               // await httpClient.PostAsync("https://www.ordinarygeeks.com/api/questions/", new StringContent(JsonConvert.SerializeObject(questions[index]).ToString(), Encoding.UTF8, "application/json"));
                index++;

            }
            await httpClient.PostAsync("https://www.ordinarygeeks.com/api/questions/multipartupload", new StringContent(JsonConvert.SerializeObject(questions).ToString(), Encoding.UTF8, "application/json"));

        }
    }
}
