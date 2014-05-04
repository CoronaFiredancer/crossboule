using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBoule.Services
{
	public interface IFileStorageService
	{
		Task<bool> FileExistAsync(string path);
		Task SaveImageAsync(Stream stream, string path);
	}
}
