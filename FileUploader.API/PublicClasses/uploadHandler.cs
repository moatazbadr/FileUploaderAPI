namespace FileUploader.API.PublicClasses
{
    public class uploadHandler
    { 


        public string Upload(IFormFile file)
        {
            //extension
            List<string> validExtensions = new List<string>() {".jpg",".gif",".png"};
            string extension= Path.GetExtension(file.FileName);
            if (!validExtensions.Contains(extension)) {
                return $"Extensiton is not valid only valid is ({string.Join(',', validExtensions)}) ";
            }
            //FileSize of 10mb
            long size =file.Length;
            if (size > (10 * 1024 * 1024)) {
                return "File size limit exceeded";
            }

            //name changing
            string FileName= Guid.NewGuid().ToString();
            string path=Path.Combine(Directory.GetCurrentDirectory(),"Uploads");
            //will atomatically close the stream
            using  FileStream stream = new FileStream(Path.Combine(path,FileName),FileMode.Create);
            file.CopyTo( stream);    
            //stream.Dispose();
            //stream.Close();

            return FileName;

        }
    }
}
