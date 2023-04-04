﻿using Ark.Infrastructure.Shared.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Infrastructure.Shared.Controllers;

/// <summary>
///     File endpoints
/// </summary>
[Route("[controller]")]
public class FilesController : ApiBaseController
{
    /// <summary>
    ///     Создать статью
    /// </summary>
    [HttpGet("images/{imageName}")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(SharedLib.Common.Results.Result))]
    [Produces("application/octet-stream", Type = typeof(FileResult))]
    public async Task<IActionResult> GetImage([FromRoute] string imageName)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", imageName);
        var memory = new MemoryStream();
        await using (var stream = new FileStream(path, FileMode.Open))
            await stream.CopyToAsync(memory);
        memory.Position = 0;
        return File(memory, GetContentType(path), Path.GetFileName(path));
    }

    private string GetContentType(string path)
    {
        var types = GetMimeTypes();
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }

    private Dictionary<string, string> GetMimeTypes() =>
        new Dictionary<string, string>
        {
            {".txt", "text/plain"},
            {".pdf", "application/pdf"},
            {".doc", "application/vnd.ms-word"},
            {".docx", "application/vnd.ms-word"},
            {".xls", "application/vnd.ms-excel"},
            {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".gif", "image/gif"},
            {".csv", "text/csv"},
        };
}