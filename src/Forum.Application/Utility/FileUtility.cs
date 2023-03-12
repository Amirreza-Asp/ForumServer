namespace Forum.Application.Utility
{
    public static class FileUtility
    {
        public static String ExtensionFromBase64(string base64)
        {
            return base64.Split(';')[0].Split('/')[1];
        }

        public static String GetData(string base64)
        {
            return base64.Replace("data:", "")
                .Replace("/^.+,/", "");
        }

        public static String ReplaceExtension(this String file, String extension)
        {
            return file.Replace(Path.GetExtension(extension), extension);
        }
    }
}
