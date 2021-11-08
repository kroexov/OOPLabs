using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;

namespace Backups.Services
{
    public class RepositoryManager : IRepository
    {
        private List<Repository> _repositories = new List<Repository>();
        private string _curDir = Directory.GetCurrentDirectory();
        public Repository AddRepository(string name)
        {
            string path = _curDir + @"\BackupRoot" + name;
            Directory.CreateDirectory(path);
            Repository repository = new Repository(path);
            return repository;
        }

        public string SingleStorage(List<string> paths)
        {
            string newpath = _curDir + @"\temp";
            Directory.CreateDirectory(newpath);
            foreach (var path in paths)
            {
                DirectoryInfo copy = new DirectoryInfo(path);
                foreach (var file in copy.GetFiles())
                {
                    file.CopyTo(newpath + file.ToString().Substring(file.ToString().LastIndexOf(@"\")));
                }
            }

            return newpath;
        }

        public void DeleteStorage(string path)
        {
            DirectoryInfo temp = new DirectoryInfo(path);
            foreach (var file in temp.GetFiles())
            {
                file.Delete();
            }

            temp.Delete();
        }

        public void AddZipArchive(string path, string destination)
        {
            ZipFile.CreateFromDirectory(path, destination);
        }
    }
}