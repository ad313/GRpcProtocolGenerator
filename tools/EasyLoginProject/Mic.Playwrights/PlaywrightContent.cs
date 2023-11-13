using Microsoft.Playwright;

namespace Mic.Playwrights
{
    public class PlaywrightContent : IAsyncDisposable
    {
        public Guid Id { get; private set; }

        public IPlaywright Playwright { get; private set; }

        public IBrowser Browser { get; private set; }

        public IBrowserContext Context { get; private set; }

        public InstanceConfig InstanceConfig { get; private set; }

        public PlaywrightContent(IPlaywright playwright, IBrowser browser, IBrowserContext context)
        {
            Id = Guid.NewGuid();
            Playwright = playwright;
            Browser = browser;
            Context = context;
        }

        public void SetInstanceConfig(InstanceConfig config)
        {
            InstanceConfig = config;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.</summary>
        /// <returns>A task that represents the asynchronous dispose operation.</returns>
        public async ValueTask DisposeAsync()
        {
            await Context.DisposeAsync();
            await Browser.DisposeAsync();

            if (Playwright is IAsyncDisposable playwrightAsyncDisposable)
                await playwrightAsyncDisposable.DisposeAsync();
            else
                Playwright.Dispose();

            PlaywrightManager.RemoveContent(this);
        }
    }
}