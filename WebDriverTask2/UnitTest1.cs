using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace WebDriverTask2
{
    public class PasterbinTest
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
        }

        [Test]
        public void CreateNewPaste()
        {
            driver.Url = "https://pastebin.com";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
          
            IWebElement pasteGitCommand = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("postform-text")));
            pasteGitCommand.SendKeys("git config --global user.name New Sheriff in Town git reset $(git commit-tree HEAD^{tree} -m Legacy code)git push origin master --force");

            IWebElement chooseBash = driver.FindElement(By.Id("select2-postform-format-container"));
            chooseBash.Click();
            driver.FindElement(By.XPath("//li[contains(text(),'Bash')]")).Click();


            IWebElement expirationTime = driver.FindElement(By.Id("select2-postform-expiration-container"));
            expirationTime.Click();
            driver.FindElement(By.XPath("//li[contains(text(),'10 Minutes')]")).Click();

            IWebElement titleField = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("postform-name")));
            titleField.SendKeys("how to gain dominance among developers");

            IWebElement createPasteButton = driver.FindElement(By.XPath("//button[contains(text(),'Create New Paste')]"));
            createPasteButton.Click();

            WebDriverWait waitForTitle = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            waitForTitle.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains("how to gain dominance among developers"));
            
            Assert.That(driver.Title, Is.EqualTo("how to gain dominance among developers - Pastebin.com"));

            IWebElement syntaxIsCorrect = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(".bash")));
            Assert.IsNotNull(syntaxIsCorrect, "Syntax highlighting for Bash was not found.");

            IWebElement codeContent = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//div[@class='de1']")));
            string codeText = codeContent.Text.Replace("\u00A0", " ");

            string expectedCode = "git config --global user.name New Sheriff in Town git reset $(git commit-tree HEAD^{tree} -m Legacy code)git push origin master --force".Replace("\u00A0", " "); ;
            Assert.That(codeText, Is.EqualTo(expectedCode), "The code content does not match the expected value.");

        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}