//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using OpenAI.Managers;
//using OpenAI;
//using OpenAI.ObjectModels.RequestModels;
//using OpenAI.Interfaces;

//namespace IHMS.APIControllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class QuestionController : ControllerBase
//    {
//        [HttpGet]
//        public async Task<IActionResult> GetQuestionAsync()
//        {
//            var completionResult = await openAiService.Completions.CreateCompletion(new CompletionCreateRequest()
//            {
//                Prompt = "Once upon a time",
//                Model = Models.TextDavinciV3
//            });

//            if (completionResult.Successful)
//            {
//                Console.WriteLine(completionResult.Choices.FirstOrDefault());
//            }
//            else
//            {
//                if (completionResult.Error == null)
//                {
//                    throw new Exception("Unknown Error");
//                }
//                Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
//            }
//            return null;
//        }
//        //[HttpGet]
//        //public IActionResult GetQuestion()
//        //{
//        //    var openAiService = new OpenAIService(new OpenAiOptions()
//        //    {
//        //        ApiKey = Environment.GetEnvironmentVariable("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")
//        //    });

//        //    return null;
//        //}
//    }
//}
