namespace MobileBG.Web.Controllers
{
    public class ImageController : BaseController
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpPost]
        [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(string url)
        {
            await this.imageService.DeleteImageAsync(url);
            return this.Ok();
        }
    }
}
