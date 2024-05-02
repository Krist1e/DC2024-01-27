using System.Globalization;
using Asp.Versioning;
using Discussion.Dto.Request;
using Discussion.Dto.Response;
using Discussion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Discussion.Controllers;

[Route("api/v{version:apiVersion}/posts")]
[ApiVersion("1.0")]
[ApiController]
public class PostController(IPostService service) : ControllerBase
{
    [HttpGet("{id:long}")]
    public async Task<ActionResult<PostResponseTo>> GetPost(long id)
    {
        return Ok(await service.GetPostById(id));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostResponseTo>>> GetPosts()
    {
        return Ok(await service.GetPosts());
    }

    [HttpPost]
    public async Task<ActionResult<PostResponseTo>> CreatePost(PostRequestTo postRequestTo)
    {
        PostResponseTo addedPost = await service.CreatePost(GetRequestWithCountry(postRequestTo));
        return CreatedAtAction(nameof(GetPost), new
        {
            id = addedPost.Id
        }, addedPost);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeletePost(long id)
    {
        await service.DeletePost(id);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult<PostResponseTo>> UpdatePost(PostRequestTo updatePostRequestTo)
    {
        return Ok(await service.UpdatePost(GetRequestWithCountry(updatePostRequestTo)));
    }

    private PostRequestTo GetRequestWithCountry(PostRequestTo requestTo) =>
        string.IsNullOrEmpty(requestTo.Country)
            ? requestTo with { Country = GetCountryFromRequest() }
            : requestTo;

    private string GetCountryFromRequest()
    {
        var lang = Request.GetTypedHeaders().AcceptLanguage
            .MaxBy(x => x.Quality ?? 1)
            ?.Value.ToString();
        
        return string.IsNullOrEmpty(lang) ? "Unknown" : lang.Length > 2 ? new CultureInfo(lang).TwoLetterISOLanguageName : lang;
    }
}
