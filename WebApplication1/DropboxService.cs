using Dropbox.Api;
using Dropbox.Api.Files;
using System.Collections.Generic;
using System.Linq;

public class DropboxService
{
    private DropboxClient _client;

    // Initialize with your access token
    public DropboxService(string accessToken)
    {
        _client = new DropboxClient(accessToken);
    }

    // Get all files/folders in a Dropbox path
    public List<Metadata> GetFiles(string path = "")
    {
        var list = _client.Files.ListFolderAsync(path).Result;
        return list.Entries.ToList();
    }
}