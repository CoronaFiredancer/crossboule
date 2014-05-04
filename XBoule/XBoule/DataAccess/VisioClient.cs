using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XBoule.Services;

namespace XBoule.DataAccess
{
	public class VisioClient
	{
		IFileStorageService fileStorage;
		private HttpClient client;

		public VisioClient(IFileStorageService fileStorage)
		{
			client = new HttpClient();
			this.fileStorage = fileStorage;
		}

		public async Task<string> GetImage(string httpAddress, string storagePath)
		{
			try
			{
				if (await fileStorage.FileExistAsync(storagePath))
					return storagePath;

				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, httpAddress);
				request.Headers.UserAgent.ParseAdd("Windows Mobile");
				//HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
				var response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;
				using (var stream = await response.Content.ReadAsStreamAsync())
				{
					await fileStorage.SaveImageAsync(stream, storagePath);
					return storagePath;
				}

				//using (var stream = await client.GetStreamAsync(httpAddress))
				//{
				//    fileStorage.SaveImage(stream, storagePath);
				//    return storagePath;
				//}
			}
			catch (Exception ex)
			{
				
				return storagePath;
			}

		}
	}
}
