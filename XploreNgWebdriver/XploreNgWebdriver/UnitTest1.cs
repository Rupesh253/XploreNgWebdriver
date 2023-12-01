using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Protractor;
using System.Collections.ObjectModel;
using System.Security.Principal;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using OpenQA.Selenium.DevTools.V114.Schema;
using Newtonsoft.Json.Schema;
using NUnit.Framework.Constraints;
using OpenQA.Selenium.DevTools.V114.ServiceWorker;
using System.Diagnostics;
using System.Collections.Immutable;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V114.Performance;
using EnableCommandSettings = OpenQA.Selenium.DevTools.V114.Performance.EnableCommandSettings;

namespace XploreNgWebdriver {
    public class Tests {
        [SetUp]
        public void Setup() {
        }
        public async Task LogNetworkRequests(IWebDriver driver) {
            INetwork interceptor = driver.Manage().Network;
            interceptor.NetworkRequestSent += OnNetworkRequestSent;
            interceptor.NetworkResponseReceived += OnNetworkResponseReceived;
            await interceptor.StartMonitoring();
            driver.Url = "https://demo1755509.mockable.io/json1";
            await interceptor.StopMonitoring();
        }
        [Test]
        public void Test1() {
            //IWebDriver driver = new ChromeDriver(@"C:\Users\git\source\XploreNgWebdriver\XploreNgWebdriver\bin\Debug\net6.0\");
            string jsonSchema = @"{
  ""$schema"": ""http://json-schema.org/draft-07/schema#"",
  ""title"": ""Generated schema for Root"",
  ""type"": ""object"",
  ""properties"": {
    ""name"": {
      ""type"": ""string""
    },
    ""hobbies"": {
      ""type"": ""array"",
      ""items"": {
        ""type"": ""string""
      }
    }
  },
  ""required"": [
    ""name"",
    ""hobbies""
  ]
}";

            JSchema schema = JSchema.Parse(jsonSchema);
            JObject person = JObject.Parse(@"{
  'hobbies': ['.NET', 'Blogging', 'Reading', 'Xbox', 'LOLCATS',122,true]
}");
            IList<string> messages;

            bool isvalid = person.IsValid(schema, out messages);
            Console.WriteLine($"schema validation:{isvalid}");
            StringBuilder messaegesListInString = new StringBuilder();
            foreach (string m in messages) {
                messaegesListInString.AppendLine("*********************");
                messaegesListInString.AppendLine(m);
                Console.WriteLine(m);
            }

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            IWebDriver driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(3));
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(3);
            NgWebDriver ngDriver = new NgWebDriver(driver);
            ngDriver.IgnoreSynchronization = false;
            ngDriver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(3);
            //LogNetworkRequests(driver);

            /*
            ngDriver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/Multiform/#/form/profile");
            ngDriver.WaitForAngular();
            ngDriver.FindElement(NgBy.Model("formData.name")).SendKeys("ng Name");
            ngDriver.FindElement(new NgByModel("formData.email")).SendKeys("rupesh@gmail.com");
            //driver.FindElement(By.Name("name")).SendKeys("normal name");
            // Thread.Sleep(2000);
            driver.FindElement(By.CssSelector(".btn.btn-block.btn-info")).Click();
            ngDriver.WaitForAngular();
            driver.FindElement(By.XPath("(//input[@ng-model='formData.type'])[2]")).Click();
            ngDriver.WaitForAngular();
            driver.FindElement(By.XPath("//*[contains(text(),'Next Section')]")).Click();
            Thread.Sleep(2000);
            driver.Quit();
            */

            /*
            ngDriver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/WebTable/");
            ngDriver.WaitForAngular();
            //driver.FindElement(By.XPath("//[@placeholder='search for firstname']")).SendKeys("Pierre");
            int i = 1;
            ReadOnlyCollection<NgWebElement> rows = ngDriver.FindElements(NgBy.ExactRepeater("row in rowCollection"));
            foreach (NgWebElement row in rows) {
                ReadOnlyCollection<NgWebElement> columns = row.FindElements(NgBy.ExactRepeater("col in columns"));
                Console.Write($"{String.Format("{0,-8}", $"Row{i}: ")}");
                int j = 1;
                foreach (NgWebElement column in columns) {
                    Console.Write($"{string.Format("{0,-16}", $"{j}." + column.Text)}\t");
                    j++;
                }
                i++;
                Console.WriteLine();
            }
            Console.WriteLine(driver.FindElement(By.XPath("(//*[@ng-repeat='col in columns'])[2]")).Text);
            Console.WriteLine(driver.FindElement(By.XPath("(//*[@ng-repeat='row in rowCollection'])[2]")).Text);
            Console.WriteLine($"First Row:{ngDriver.FindElement(NgBy.Repeater("row in rowCollection")).Text}");
            Console.WriteLine($"First column:{ngDriver.FindElement(NgBy.Repeater("col in columns")).Text}");
            */

            /*
            ngDriver.Navigate().GoToUrl("https://www.globalsqa.com/angularJs-protractor/SearchFilter/");
            //ngDriver.FindElement(NgBy.Model("filterTxn.payee.name")).SendKeys("S");
            //Thread.Sleep(1000);
            //ngDriver.FindElement(NgBy.Model("filterTxn.payee.name")).SendKeys("a");
            Thread.Sleep(1000);
            SelectElement select = new SelectElement(ngDriver.FindElement(NgBy.Model("filterTxn.account")));
            Console.WriteLine($"Is multiselect: {select.IsMultiple}");
            //select.SelectByIndex(1);
            //select.SelectByValue("2");
            select.SelectByText("Cash");
            //select.DeselectByIndex(1);
            Console.WriteLine($"{select.SelectedOption.Text}");
            */

            string text = File.ReadAllText(@"C:\Users\git\source\XploreNgWebdriver\XploreNgWebdriver\test.json");
            JObject obj = JObject.Parse(text);
            JToken tbToken = obj.SelectToken("$.");
            Assert.Multiple(() => {
                //Assert.AreEqual(typeof(object), typeof(tbToken), "");
                //Assert.IsTrue(tbToken.SelectToken("id").ToString());
                //Assert.IsNull(tbToken, "is not null");
            }
            );
            Console.WriteLine(tbToken.ToString());
            //Console.Beep();

            driver.Quit();

            /*
            ngDriver.Navigate().GoToUrl("");

            //driver.FindElement(By.Id("file-upload-button")).Click();
            ngDriver.WaitForAngular();
            //driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "a");
            //driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "c");
            //string text = Clipboard.GetText();
            //SendKeys.SendWait(GetProjectRoot() + @"\DataFiles\red.png" + @"{RIGHT}");
            Console.WriteLine($"~~~[before]Active{driver.CurrentWindowHandle}"); //
            driver.FindElement(By.XPath("//*[contains(text(),'Sign In')]")).Click();
            ngDriver.WaitForAngular();
            ReadOnlyCollection<string> handles = driver.WindowHandles;
            foreach (string handle in handles) {
                Console.WriteLine($"handle:{handle}"); //
            }
            Console.WriteLine($"Active{driver.CurrentWindowHandle}");
            driver.SwitchTo().Window(handles[1]);
            //ngDriver.WaitForAngular();
            Thread.Sleep(6000);
            driver.FindElement(By.CssSelector("#domainParameter")).SendKeys("");
            driver.FindElement(By.Id("continue")).Click();
            //ngDriver.WaitForAngular();
            Thread.Sleep(6000);
            Console.WriteLine($"*****[before]Active{driver.CurrentWindowHandle}"); //
            ReadOnlyCollection<string> handles1 = driver.WindowHandles;
            foreach (string handle in handles1) {
                Console.WriteLine($"handle:{handle}"); //
            }
            Console.WriteLine($"Active{driver.CurrentWindowHandle}");
            Thread.Sleep(5000);
            driver.SwitchTo().Window(handles1[1]);


            driver.FindElement(By.Id("<<<>>>>")).Click();
            Thread.Sleep(10000);
            driver.SwitchTo().Window(handles1[0]);
            driver.FindElement(By.XPath("//*[@placeholder='Input your Tenant (or) Product Code']")).SendKeys("<<<>>>>");
            driver.FindElement(By.XPath("//button[contains(text(),'Continue')]")).Click();
            ngDriver.WaitForAngular();
            driver.Manage().Window.Maximize();
            driver.FindElement(By.XPath("//li[@ng-reflect-router-link='../formflow/']")).Click();
            ngDriver.WaitForAngular();
            driver.FindElement(By.XPath("(//button[@title='Edit'])[3]")).Click();
            ngDriver.WaitForAngular();
            driver.FindElement(By.Id("btn-form-edit")).Click();
            ngDriver.WaitForAngular();
            Actions actions = new Actions(driver);
            IWebElement tb = driver.FindElement(By.Id("<<<<>>>>>"));
            IWebElement body = driver.FindElement(By.XPath("<<<>>>>>"));
            actions.DragAndDrop(tb, body).Build().Perform();
            ngDriver.WaitForAngular();
            driver.FindElement(By.CssSelector("<<<<>>>>>")).Click();
            ngDriver.WaitForAngular();
            driver.FindElement(By.XPath("//div[@title='__DEVICEID : __DeviceID']")).Click();
            IWebElement save = driver.FindElement(By.CssSelector("btn.btn-success"));
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].scrollIntoView(true)", save);
            save.Click();
            */
            string actual = "rupeSh";
            string expected = "rupesh";


            using (new AssertionScope()) {
                actual.Should().BeNull();
                actual.Should().Be(expected);
                actual.Should().BeEquivalentTo(expected);
                actual.Should().NotBeNull();
                actual.Should().BeEmpty();
                actual.Should().BeNullOrWhiteSpace();
                actual.Should().BeLowerCased();
                actual.Should().BeUpperCased();
                actual.Should().BeAssignableTo(typeof(int));
                actual.Should().BeAssignableTo<int>();
                actual.Should().BeOfType(typeof(int));
                actual.Should().BeOfType<int>();
                actual.Should().Contain("");
                actual.Should().StartWith("");
                actual.Should().StartWithEquivalentOf("R=");
                actual.Should().EndWith("");
                actual.Should().EndWithEquivalentOf("h").Should().BeNull();
                actual.Should().NotBeNull()
                    .Should().NotBeOfType(typeof(int))
                    .Should().Be(expected);
                actual.Should().ContainAll("1", "2");
            }
        }

        [Test]
        public void Test2() {

            int integer1 = 5;
            int integer2 = 3;


            using (new AssertionScope()) {

                //
                integer1.Should().BePositive();
                integer1.Should().BeNegative();
                integer1.Should().BeGreaterThan(4);
                integer1.Should().BeGreaterThanOrEqualTo(52);
                integer2.Should().BeLessThan(0);
                integer1.Should().BeLessThanOrEqualTo(4);
                integer1.Should().BeInRange(1, 5);
                integer1.Should().BeOfType(typeof(int));
                integer1.Should().BeOneOf(1, 2, 3);
                integer1.Should().Be(integer2);
                //

            }
        }
        [Test]
        public void Test3() {

            bool isTrue = true;
            bool isFalse = false;


            using (new AssertionScope()) {

                isTrue.Should().BeFalse();
                isTrue.Should().BeTrue();
                isTrue.Should().Be(false);

            }
        }

        [TestCaseSource(nameof(source))]
        [Test]
        public void Test4(string a) {

            string[] stringArray = new string[] { "a", "b", "c", "a" };
            int[] intArray = new int[] { 1, 2, 3 };
            bool[] boolArray = new bool[] { true, false, true, false };
            object[] objectArray = new object[] { "", "" };

            using (new AssertionScope()) {

                stringArray.Should().AllBe("a5");
                stringArray.Should().AllBeAssignableTo(typeof(int));
                stringArray.Should().AllBeAssignableTo<int>();
                stringArray.Should().AllBeEquivalentTo("v");
                stringArray.Should().OnlyHaveUniqueItems();
            }
        }
        private void OnNetworkResponseReceived(object? sender, NetworkResponseReceivedEventArgs e) {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Response {0}", e.RequestId).AppendLine();
            builder.AppendLine("--------------------------------");
            builder.AppendFormat("{0} {1}", e.ResponseStatusCode, e.ResponseUrl).AppendLine();
            foreach (KeyValuePair<string, string> header in e.ResponseHeaders) {
                builder.AppendFormat("{0}: {1}", header.Key, header.Value).AppendLine();
            }

            if (e.ResponseResourceType == "Document") {
                builder.AppendLine(e.ResponseBody);
            } else if (e.ResponseResourceType == "Script") {
                builder.AppendLine("<JavaScript content>");
            } else if (e.ResponseResourceType == "Stylesheet") {
                builder.AppendLine("<stylesheet content>");
            } else if (e.ResponseResourceType == "Image") {
                builder.AppendLine("<image>");
            } else {
                builder.AppendFormat("Content type: {0}", e.ResponseResourceType).AppendLine();
            }

            builder.AppendLine("--------------------------------");
            builder.Append(e.ResponseBody);
            Console.WriteLine(builder.ToString());
        }

        private void OnNetworkRequestSent(object? sender, NetworkRequestSentEventArgs e) {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Request {0}", e.RequestId).AppendLine();
            builder.AppendLine("--------------------------------");
            builder.AppendFormat("{0} {1}", e.RequestMethod, e.RequestUrl).AppendLine();
            foreach (KeyValuePair<string, string> header in e.RequestHeaders) {
                builder.AppendFormat("{0}: {1}", header.Key, header.Value).AppendLine();
            }
            builder.AppendLine("--------------------------------");
            builder.AppendLine();
            Console.WriteLine(builder.ToString());
        }

        public static string[] source = new string[] { "1", "2" };
        public static object[] sourcesss = new object[] {
            new object[] { "name","name" } ,
            new object[] { 4,5,6}
        };
        [Test]
        public void Test5() {
            IWebDriver driver = new ChromeDriver();
            Dictionary<string, int> searchTimes = new Dictionary<string, int>();
            driver.Url = "https://www.saucedemo.com/?ref=hackernoon.com";
            Thread.Sleep(3000);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            driver.FindElement(By.Id("user-name"));
            stopwatch.Stop();
            searchTimes.Add("ID", Convert.ToInt32(stopwatch.ElapsedMilliseconds));
            Console.WriteLine($"Time taken by id: {stopwatch.ElapsedMilliseconds}ms");
            stopwatch.Reset();
            stopwatch.Start();
            driver.FindElement(By.ClassName("input_error"));
            stopwatch.Stop();
            searchTimes.Add("CLASSNAME", Convert.ToInt32(stopwatch.ElapsedMilliseconds));
            Console.WriteLine($"Time taken by class: {stopwatch.ElapsedMilliseconds}ms");
            stopwatch.Reset();
            stopwatch.Start();
            driver.FindElement(By.Name("user-name"));
            stopwatch.Stop();
            searchTimes.Add("NAME", Convert.ToInt32(stopwatch.ElapsedMilliseconds));
            Console.WriteLine($"Time taken by name: {stopwatch.ElapsedMilliseconds}ms");
            stopwatch.Reset();
            stopwatch.Start();
            driver.FindElement(By.TagName("input"));
            stopwatch.Stop();
            searchTimes.Add("TAGNAME", Convert.ToInt32(stopwatch.ElapsedMilliseconds));
            Console.WriteLine($"Time taken by tagname: {stopwatch.ElapsedMilliseconds}ms");
            /*
            stopwatch.Reset();
            stopwatch.Start();
            driver.FindElement(By.LinkText(""));
            stopwatch.Stop();
            searchTimes.Add("ID", Convert.ToInt32(stopwatch.ElapsedMilliseconds));
            Console.WriteLine($"Time taken by linkText: {stopwatch.ElapsedMilliseconds}ms");
            stopwatch.Reset();
            stopwatch.Start();
            driver.FindElement(By.PartialLinkText(""));
            stopwatch.Stop();
            searchTimes.Add("ID", Convert.ToInt32(stopwatch.ElapsedMilliseconds));
            Console.WriteLine($"Time taken by partiallinkText: {stopwatch.ElapsedMilliseconds}ms");
            */
            stopwatch.Reset();
            stopwatch.Start();
            driver.FindElement(By.XPath("//input[@id='user-name']"));
            stopwatch.Stop();
            searchTimes.Add("XPATH", Convert.ToInt32(stopwatch.ElapsedMilliseconds));
            Console.WriteLine($"Time taken by xpath: {stopwatch.ElapsedMilliseconds}ms");
            stopwatch.Reset();
            stopwatch.Start();
            driver.FindElement(By.CssSelector("#user-name"));
            stopwatch.Stop();
            searchTimes.Add("CSS", Convert.ToInt32(stopwatch.ElapsedMilliseconds));
            Console.WriteLine($"Time taken by cssSelector: {stopwatch.ElapsedMilliseconds}ms");
            driver.Quit();
            var ordered = searchTimes.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            foreach (var item in ordered) {
                Console.Write($"{item.Key}({item.Value})<");
            }

        }

        [Test]
        public void Test6() {
            Stopwatch timer = new Stopwatch();
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(50);
            //Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            driver.Navigate().GoToUrl("https://www.saucedemo.com/?ref=hackernoon.com");
            timer.Start();
            // Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            driver.FindElement(By.Id("user-name"));
            //Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            driver.FindElement(By.Id("password"));
            //Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            driver.FindElement(By.Id("login-button"));
            //Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            timer.Stop();
            Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            driver.Quit();
        }
        [Test]
        public void Test7() {
            Stopwatch timer = new Stopwatch();
            IWebDriver driver = new ChromeDriver();
            // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            //Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            driver.Navigate().GoToUrl("https://www.saucedemo.com/?ref=hackernoon.com");
            timer.Start();
            //Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            driver.FindElement(By.Id("user-name"));
            //Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            driver.FindElement(By.Id("password"));
            //Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            driver.FindElement(By.Id("login-button"));
            //Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            timer.Stop();
            Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            driver.Quit();
        }
        [Test]
        public void Test8() {
            Stopwatch timer = new Stopwatch();
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/?ref=hackernoon.com");
            timer.Start();
            IWebElement userName = driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).Above(By.Id("password")));
            IWebElement password = driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).Below(By.Id("user-name")));
            // var cancelButton = RelativeBy.WithLocator(By.TagName("button")).LeftOf(By.Id("submit"));
            //var submitButton = RelativeBy.WithLocator(By.TagName("button")).RightOf(By.Id("cancel"));
            //nearby 50px 
            //IWebElement login = driver.FindElement(RelativeBy.WithLocator(By.TagName("button")).Near(By.XPath("//label[text()='Swag Labs']")));
            //IWebElement login = driver.FindElement(RelativeBy.WithLocator(By.TagName("button")).Near(By.Id("user-name")));
            //IWebElement login = driver.FindElement(RelativeBy.WithLocator(By.TagName("button")).Near(By.Id("password")));
            userName.SendKeys("standard_user");
            password.SendKeys("secret_sauce");
            timer.Stop();
            Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            //login.Click();
            Thread.Sleep(1);
            driver.Quit();
        }
        [Test]
        public void Test9() {
            Stopwatch timer = new Stopwatch();
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/?ref=hackernoon.com");
            timer.Start();
            IWebElement userName = driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).Above(By.Id("password")));
            userName.SendKeys("standard_user");
            IWebElement password = driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).Below(By.Id("user-name")));
            password.SendKeys("secret_sauce");
            timer.Stop();
            Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            Thread.Sleep(1);
            driver.Quit();
        }
        [Test]
        public void Test10() {
            Stopwatch timer = new Stopwatch();
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://www.saucedemo.com/?ref=hackernoon.com");
            timer.Start();
            driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).Above(By.Id("password"))).SendKeys("standard_user");
            driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).Below(By.Id("user-name"))).SendKeys("secret_sauce");
            timer.Stop();
            Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            Thread.Sleep(1);
            driver.Quit();
        }
        [Test]
        public void Test10A() {
            Stopwatch timer = new Stopwatch();
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/?ref=hackernoon.com");
            timer.Start();
            driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).Above(By.Id("password"))).SendKeys("standard_user");
            driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).Below(By.Id("user-name"))).SendKeys("secret_sauce");
            timer.Stop();
            Console.WriteLine($"Time took: {timer.ElapsedMilliseconds}ms");
            Thread.Sleep(1);
            driver.Quit();

        }

        [Test]
        public async Task Test11() {
            IWebDriver driver = new ChromeDriver();
            IDevTools devTools = driver as IDevTools;
            DevToolsSession session = devTools.GetDevToolsSession();
            await session.SendCommand<EnableCommandSettings>(new EnableCommandSettings());
            var metricsResponse =
                await session.SendCommand<GetMetricsCommandSettings, GetMetricsCommandResponse>(
                    new GetMetricsCommandSettings());

            driver.Navigate().GoToUrl("https://www.saucedemo.com/?ref=hackernoon.com");
            using IJavaScriptEngine monitor = new JavaScriptEngine(driver);
            List<string> consoleMessages = new List<string>();
            monitor.JavaScriptConsoleApiCalled += (sender, e) => {
                Console.WriteLine("Log: {0}", e.MessageContent);
            };
            await monitor.StartEventMonitoring();
            driver.Quit();

            var metrics = metricsResponse.Metrics;
            foreach (Metric metric in metrics) {
                Console.WriteLine("{0} = {1}", metric.Name, metric.Value);
            }
        }

        [Test]
        public async Task Test12() {
            IWebDriver driver = new ChromeDriver();
            var handler = new NetworkRequestHandler() {
                RequestMatcher = httprequest => true,
                ResponseSupplier = http => new() {
                    StatusCode = 200,
                    Body = "Creamy, delicious cheesasasae!"
                }
            };
            INetwork networkInterceptor = driver.Manage().Network;
            networkInterceptor.AddRequestHandler(handler);
            await networkInterceptor.StartMonitoring();
            driver.Navigate().GoToUrl("https://www.selenium.dev");
            await networkInterceptor.StopMonitoring();
        }
        [Test]
        public async Task Test13() {
            IWebDriver driver = new ChromeDriver();
            var handler = new NetworkRequestHandler() {
                RequestMatcher = httprequest => true,
                ResponseSupplier = http => new() {
                    StatusCode = 200,
                    Body = "Creamy, delicious cheese!"
                }
            };

            INetwork networkInterceptor = driver.Manage().Network;
            networkInterceptor.AddRequestHandler(handler);

            await networkInterceptor.StartMonitoring();
            driver.Navigate().GoToUrl("https://www.selenium.dev");
            await networkInterceptor.StopMonitoring();
        }
        [Test]
        public async Task Test14() {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/?ref=hackernoon.com");
            using IJavaScriptEngine monitor = new JavaScriptEngine(driver);
            List<string> consoleMessages = new List<string>();
            monitor.JavaScriptConsoleApiCalled += (sender, e) => {
                Console.WriteLine("Log: {0}", e.MessageContent);
            };
            await monitor.StartEventMonitoring();
            driver.Navigate().GoToUrl("<<>>>>");
            //driver.Quit();
        }

        [Test]
        public async Task Test15() {
            IWebDriver driver = new ChromeDriver();

            List<string> exceptionMessages = new List<string>();
            using IJavaScriptEngine monitor = new JavaScriptEngine(driver);
            monitor.JavaScriptExceptionThrown += (sender, e) => {
                exceptionMessages.Add(e.Message);
            };

            await monitor.StartEventMonitoring();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/?ref=hackernoon.com");
            IWebElement link2click = driver.FindElement(By.LinkText("<your link text>"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);",
                  link2click, "onclick", "throw new Error('Hello, world!')");
            link2click.Click();

            foreach (string message in exceptionMessages) {
                Console.WriteLine("JS exception message: {0}", message);
            }
        }

        [Test]
        public async Task Test16() {
            IWebDriver driver = new ChromeDriver();
            List<DomMutationData> attributeValueChanges = new List<DomMutationData>();
            DefaultWait<List<DomMutationData>> wait = new DefaultWait<List<DomMutationData>>(attributeValueChanges);
            wait.Timeout = TimeSpan.FromSeconds(3);

            using IJavaScriptEngine monitor = new JavaScriptEngine(driver);
            monitor.DomMutated += (sender, e) => {
                attributeValueChanges.Add(e.AttributeData);
            };
            await monitor.StartEventMonitoring();

            driver.Navigate().GoToUrl("http://www.google.com");
            IWebElement span = driver.FindElement(By.CssSelector("span"));

            await monitor.EnableDomMutationMonitoring();
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute('cheese', 'gouda');", span);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute('cheese1', 'gouda1');", span);
            wait.Until((list) => list.Count > 0);
            Console.WriteLine("Found {0} DOM mutation events", attributeValueChanges.Count);
            foreach (var record in attributeValueChanges) {
                Console.WriteLine("Attribute name: {0}", record.AttributeName);
                Console.WriteLine("Attribute value: {0}", record.AttributeValue);
            }

            await monitor.DisableDomMutationMonitoring();
        }
    }
}
