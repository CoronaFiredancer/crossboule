using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace XBoule.Services
{
	public class PhoneFileStorageService : IFileStorageService
	{
		readonly IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

		public bool FileExist(string path)
		{
			return isolatedStorage.FileExists(path);
		}

		private void CreateDirectoryIfNotExist(string filePath)
		{
			var directories = filePath.Split('\\');
			var path = string.Empty;
			for (var i = 0; i < directories.Length - 1; i++)
			{
				if (path == string.Empty)
					path = directories[i];
				else
					path += string.Format("\\{0}", directories[i]);
				if (!isolatedStorage.DirectoryExists(path))
				{
					isolatedStorage.CreateDirectory(path);
				}
			}
		}

		public async Task SaveImageAsync(Stream stream, string path)
		{
			CreateDirectoryIfNotExist(path);
			int count;
			var buffer = new byte[1024];
			var isDone = false;

			count = await stream.ReadAsync(buffer, 0, buffer.Length);
			if (count == 0)
				return;

			using (var isfs = new IsolatedStorageFileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, isolatedStorage))
			{
				while (!isDone)
				{
					isfs.Write(buffer, 0, count);
					count = await stream.ReadAsync(buffer, 0, buffer.Length);

					if (count <= 0)
						isDone = true;
				}
			}
		}

		public Task<bool> FileExistAsync(string path)
		{
			return Task.Run(() => isolatedStorage.FileExists(path));
		}

		public void DeleteDirectoryRecursively(string dirName)
		{
			var pattern = dirName + @"\*";
			var files = isolatedStorage.GetFileNames(pattern);
			foreach (var fName in files)
			{
				isolatedStorage.DeleteFile(Path.Combine(dirName, fName));
			}
			var dirs = isolatedStorage.GetDirectoryNames(pattern);
			foreach (var dName in dirs)
			{
				DeleteDirectoryRecursively(Path.Combine(dirName, dName));
			}
			isolatedStorage.DeleteDirectory(dirName);
		}
	}
}
