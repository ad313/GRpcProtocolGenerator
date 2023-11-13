using Microsoft.Playwright;
using System.Collections.Concurrent;

namespace Mic.Playwrights
{
    public sealed class PlaywrightManager
    {
        public static readonly ConcurrentDictionary<Guid, PlaywrightContent> Contents = new ConcurrentDictionary<Guid, PlaywrightContent>();

        /// <summary>
        /// 创建一个隐私模式实例，默认全屏
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<PlaywrightContent> CreateIncognitoInstanceAsync(params string[] args)
        {
            var defaultArgs = new List<string>() { "--start-maximized" };

            if (args?.Length > 0)
            {
                defaultArgs.AddRange(args.Where(d => !defaultArgs.Contains(d)));
            }

            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = defaultArgs
            });

            var context = await browser.NewContextAsync(new BrowserNewContextOptions() { IgnoreHTTPSErrors = true, ViewportSize = ViewportSize.NoViewport });

            var content = new PlaywrightContent(playwright, browser, context);

            Contents.TryAdd(content.Id, content);

            return content;
        }

        public static void RemoveContent(PlaywrightContent content)
        {
            if (content == null) return;

            Contents.TryRemove(content.Id, out _);
        }

        public static async Task DisposedAllAsync()
        {
            foreach (var pair in Contents)
            {
                await pair.Value.DisposeAsync();
            }
        }
    }
}