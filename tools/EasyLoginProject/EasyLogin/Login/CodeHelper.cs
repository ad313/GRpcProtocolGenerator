namespace EasyLogin.Login
{
    internal class CodeHelper
    {
        private static HttpClient client = new HttpClient();

        public static async Task<string> GetCode(byte[] bytes, string url)
        {
            try
            {
                var content = new MultipartFormDataContent();

                content.Add(new ByteArrayContent(bytes), "file", "123.png");

                var data = await client.PostAsync(url, content);
                var str = await data.Content.ReadAsStringAsync();
                //if (DateTime.Now.Second % 3 == 0)
                //{
                //    str += "1";
                //}
                return str;
            }
            catch (Exception e)
            {
                Console.WriteLine("识别验证码失败：");
                return null;
            }
        }
    }
}
