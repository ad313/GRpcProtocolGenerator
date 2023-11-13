using Mic.Playwrights;
using Microsoft.Playwright;

namespace EasyLogin.Login
{
    internal class LoginHelper
    {
        internal static async Task<PlaywrightContent> AutoLogin_Tenant_Async(InstanceConfig config)
        {
            Console.WriteLine($"开始访问：{config.Name}");

            var content = await PlaywrightManager.CreateIncognitoInstanceAsync();
            content.SetInstanceConfig(config);

            var page = await content.Context.NewPageAsync();

            await page.Locator("body").ClickAsync();

            await page.GotoAsync(config.Url);

            Console.WriteLine($"正在访问网站：{config.Url}");

            //await Task.Delay(1000);

            await page.GetByPlaceholder("请输入账号").ClickAsync();

            await page.GetByPlaceholder("请输入账号").FillAsync(config.User);

            Console.WriteLine($"输入账号：{config.User}");

            await page.GetByPlaceholder("请输入密码").ClickAsync();

            await page.GetByPlaceholder("请输入密码").FillAsync(config.Password);

            Console.WriteLine($"输入账号：{config.Password}");

            await page.Locator("form svg").Nth(3).ClickAsync();

        GetCode: Console.WriteLine($"正在识别二维码：{config.CodeUrl}");

            var code = "";
            //识别二维码
            while (string.IsNullOrWhiteSpace(code))
            {
                var img = page.GetByRole(AriaRole.Img);
                var bytes = await img.ScreenshotAsync();

                //识别验证码
                code = await CodeHelper.GetCode(bytes, config.CodeUrl);

                //code += "1";

                Console.WriteLine($"识别二维码：{code}");

                if (string.IsNullOrWhiteSpace(code))
                {
                    Console.WriteLine($"识别二维码失败：{code}");

                    await Task.Delay(1000);
                }
                else
                {
                    break;
                }
            }

            //处理识别错误
            //验证码错误或已过期，请重新输入或刷新重试

            //登录
            await page.GetByPlaceholder("请输入验证码").ClickAsync();
            await page.GetByPlaceholder("请输入验证码").FillAsync(code);

            await page.GetByRole(AriaRole.Button, new() { Name = "登录" }).ClickAsync();

            var cancel = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            while (true)
            {
                var count = await page.Locator("nz-notification").CountAsync();
                if (count <= 0)
                {
                    await Task.Delay(20, cancel.Token);

                    if (cancel.Token.IsCancellationRequested)
                        break;

                    continue;
                }

                if (cancel.Token.IsCancellationRequested)
                {
                    break;
                }

                //有错误，重新拿验证码
                await Task.Delay(20, cancel.Token);
                await page.GetByRole(AriaRole.Img).ClickAsync();
                goto GetCode;
            }

            Console.WriteLine("登录。。。");

            return content;
        }

        internal static async Task<PlaywrightContent> AutoLogin_Old_Async(InstanceConfig config)
        {
            Console.WriteLine($"开始访问：{config.Name}");

            var content = await PlaywrightManager.CreateIncognitoInstanceAsync();
            content.SetInstanceConfig(config);

            var page = await content.Context.NewPageAsync();

            await page.Locator("body").ClickAsync();

            await page.GotoAsync(config.Url);

            Console.WriteLine($"正在访问网站：{config.Url}");

            //await Task.Delay(1000);

            await page.GetByPlaceholder("用户名").ClickAsync();

            await page.GetByPlaceholder("用户名").FillAsync(config.User);

            Console.WriteLine($"用户名：{config.User}");

            await page.GetByPlaceholder("密码").ClickAsync();

            await page.GetByPlaceholder("密码").FillAsync(config.Password);

            Console.WriteLine($"密码：{config.Password}");

            //await page.Locator("form svg").Nth(3).ClickAsync();

        GetCode: Console.WriteLine($"正在识别二维码：{config.CodeUrl}");

            var code = "";
            //识别二维码
            while (string.IsNullOrWhiteSpace(code))
            {
                //var img = page.GetByRole(AriaRole.Img);
                var img = page.Locator("#code");
                var bytes = await img.ScreenshotAsync();

                //识别验证码
                code = await CodeHelper.GetCode(bytes, config.CodeUrl);

                //code += "1";

                Console.WriteLine($"识别二维码：{code}");

                if (string.IsNullOrWhiteSpace(code))
                {
                    Console.WriteLine($"识别二维码失败：{code}");
                    await page.Locator("#code").ClickAsync();
                    await Task.Delay(1000);
                }
                else
                {
                    break;
                }
            }

            //处理识别错误
            //验证码错误或已过期，请重新输入或刷新重试

            //验证码
            await page.GetByRole(AriaRole.Textbox, new() { Name = "验证码" }).ClickAsync();
            await page.GetByRole(AriaRole.Textbox, new() { Name = "验证码" }).FillAsync(code);

            await page.GetByRole(AriaRole.Button, new() { Name = "登录" }).ClickAsync();

            var cancel = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            while (true)
            {
                var count = await page.GetByText("验证码不正确").CountAsync();
                if (count <= 0)
                {
                    await Task.Delay(20, cancel.Token);

                    if (cancel.Token.IsCancellationRequested)
                        break;

                    continue;
                }

                if (cancel.Token.IsCancellationRequested)
                {
                    break;
                }

                //有错误，重新拿验证码
                await Task.Delay(20, cancel.Token);
                await page.Locator("#code").ClickAsync();
                goto GetCode;
            }

            Console.WriteLine("登录。。。");

            return content;
        }
    }
}
