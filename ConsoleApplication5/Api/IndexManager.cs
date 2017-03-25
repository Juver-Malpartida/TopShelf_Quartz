using MyService.Files;
using MyService.Files.SourceFolder;
using System.Web.Http;

namespace ConsoleApplication5.Api
{
    public class IndexManagerController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Execute()
        {
            var fileJob = new FileJob(new SourceFolderScanner());
            fileJob.Execute(null);
            return Ok();
        }
    }
}
