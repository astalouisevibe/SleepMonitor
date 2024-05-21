using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SleepMonitor
{
    public class Downloader
    {
        private readonly StorageHandler _handler;
        private readonly string _url;

        public Downloader(string groupNum)
        {
            _url = "https://us-central1-st2prj2-fileshare.cloudfunctions.net/files?group=" + groupNum;
            _handler = new StorageHandler(groupNum);
        }

        public void Load(string filename, FileStream @out)
        {
            Task.Run(() =>
            {
                var task = _handler.Download(filename);

                TaskEnd(task, @out);
            }).Wait();

        }

        public List<string> GetFilenames()
        {
            List<string> filenames = new List<string>();
            Task.Run(async () =>
            {
                try
                {
                    var httpClient = new HttpClient();
                    var data = await httpClient.GetStringAsync(_url);
                    var files = JsonSerializer.Deserialize<List<File>>(data) ?? new List<File>();
                    filenames = files
                        .Where(f => f.Filename != null)
                        .Select(f => f.Filename!)
                        .ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }).Wait();

            return filenames;
        }



        private void TaskEnd(object task, FileStream @out)
        {
            var t = task as Task<string?>;
            var content = t?.Result;
            var streamWriter = new StreamWriter(@out);
            streamWriter.Write(content);
            streamWriter.Flush();
            streamWriter.Close();
        }


    }
    public class Uploader
    {
        private readonly StorageHandler handler;

        public Uploader(string groupNum)
        {
            handler = new StorageHandler(groupNum);
        }

        public string Save(string filename, FileStream @in)
        {
            String result = "";
            // var task = handler.Upload(filename, @in);

            var task = handler.Upload(filename, @in);
            Task.Run(() =>
            {
                try
                {
                    result = TaskEnd(task);
                }
                catch (Exception)
                {
                    result = TaskEnd(task);
                }
            }).Wait();

            return result;
        }

        private string TaskEnd(object task)
        {
            var t = task as Task<string?>;
            var content = t?.Result;

            return content ?? "";
        }
    }
    internal class File
    {
        [JsonPropertyName("filename")]
        public string? Filename { get; set; }
    }
    internal class StorageHandler
    {
        private readonly string _groupNum;
        private FirebaseStorage _firebaseStorage;

        public StorageHandler(string groupNum)
        {
            _groupNum = groupNum;
            var options = new FirebaseStorageOptions();
            _firebaseStorage = new FirebaseStorage("st2prj2-fileshare.appspot.com", options);
        }

        internal async Task<string> Upload(string filename, FileStream fs)
        {
            var filenameToUse = await FindNextLegalFileName(filename);

            await _firebaseStorage.Child(_groupNum).Child(filenameToUse).PutAsync(fs)!;
            var firebaseStorageReference = await _firebaseStorage.Child(_groupNum).Child(filenameToUse).GetMetaDataAsync();
            return filenameToUse;
        }

        // internal async Task<List<string>> List()
        // {
        //     var firebaseMetaData = await _firebaseStorage.Child(_groupNum).GetMetaDataAsync();
        //     _firebaseStorage.Child(_groupNum).
        // }


        private async Task<string> FindNextLegalFileName(string filename)
        {
            if (!await IsNameUsed(filename))
            {
                return filename;
            }

            var i = 1;
            while (true)
            {
                var filenameNumbered = GetFileName(filename, i);
                if (!await IsNameUsed(filenameNumbered))
                {
                    return filenameNumbered;
                }
                i++;
            }
        }

        private async Task<bool> IsNameUsed(string filename)
        {
            try
            {
                await _firebaseStorage.Child(_groupNum).Child(filename).GetMetaDataAsync()!;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GetFileName(string filename, int i)
        {
            if (filename.Contains("."))
            {
                var index = filename.LastIndexOf(".", StringComparison.Ordinal);
                return $"{filename.Substring(0, index)}_{i}.{filename.Substring(index + 1)}";
            }

            return $"{filename}_{i}";
        }

        internal async Task<string> Download(string filename)
        {
            try
            {
                var url = await _firebaseStorage.Child(_groupNum).Child(filename).GetDownloadUrlAsync();
                using var httpClient = new HttpClient();
                return await httpClient.GetStringAsync(url);
            }
            catch (Exception)
            {
                throw new ArgumentException($"File '{filename}' do not exists. Has file been created");
            }

        }


    }
    

}






