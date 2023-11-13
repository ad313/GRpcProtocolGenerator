using EasyLogin.Login;
using EasyLogin.Models;
using Mic.Playwrights;
using Newtonsoft.Json;

namespace EasyLogin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static Config config;

        private void MainForm_Load(object sender, EventArgs e)
        {
            config = LoadConfig();
            if (config == null)
                return;

            listBoxUser.Items.Clear();
            foreach (var user in config.Users.OrderBy(d => d.Group))
            {
                foreach (var userItem in user.Items)
                {
                    listBoxUser.Items.Add($"{user.Group} | {userItem.User} | {userItem.Password}");
                }
            }

            listBoxSite.Items.Clear();
            foreach (var site in config.Sites.OrderBy(d => d.Group))
            {
                foreach (var item in site.Items)
                {
                    listBoxSite.Items.Add($"{site.Group} | {item.Name} | {item.Url}");
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Task.Run(async () =>
            {
                await PlaywrightManager.DisposedAllAsync();
            }).GetAwaiter().GetResult();
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            //获取选中的项

            var site = listBoxSite.SelectedItem;
            if (site == null)
            {
                MessageBox.Show("请选择站点");
                return;
            }

            var user = listBoxUser.SelectedItem;
            if (user == null)
            {
                MessageBox.Show("请选择用户");
                return;
            }

            var siteArray = site.ToString().Split('|');
            var siteModel = new SiteItem()
            {
                Name = siteArray[siteArray.Length - 2].Trim(),
                Url = siteArray.Last().Trim()
            };

            var userArray = user.ToString().Split('|');
            var userModel = new UserItem()
            {
                User = userArray[userArray.Length - 2].Trim(),
                Password = userArray.Last().Trim()
            };

            var instanceConfig = new InstanceConfig()
            {
                CodeUrl = config.Ocr,
                Name = siteModel.Name,
                Url = siteModel.Url,
                User = userModel.User,
                Password = userModel.Password
            };

            var instance = instanceConfig.GetKey();

            if (listBoxInstance.Items.Contains(instance))
            {
                MessageBox.Show("不要重复启动");
                return;
            }

            listBoxInstance.Items.Add(instance);

            btnStart.Enabled = false;

            Task.Run(async () =>
            {
                await Task.Delay(10);

                var isOld = config.Sites.FirstOrDefault(d => d.Items.Any(t => t.Url == instanceConfig.Url))?.Old;

                if (isOld == true)
                    await LoginHelper.AutoLogin_Old_Async(instanceConfig);
                else
                    await LoginHelper.AutoLogin_Tenant_Async(instanceConfig);
            });

            btnStart.Enabled = true;
        }

        private Config LoadConfig()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "config.json");
            if (!File.Exists(path))
            {
                MessageBox.Show("未读取到配置文件：config/config.json");
                return null;
            }

            var text = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Config>(text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (var content in PlaywrightManager.Contents)
            {
                if (!content.Value.Context.Pages.Any())
                {
                    Task.Run(async () =>
                    {
                        await content.Value.DisposeAsync();
                    });

                    if (listBoxInstance.Items.Contains(content.Value.InstanceConfig.GetKey()))
                    {
                        listBoxInstance.Items.Remove(content.Value.InstanceConfig.GetKey());
                    }
                }
            }
        }
    }
}