using Xunit;
using System;
using System.Collections.Generic;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PercyIO.Selenium;

namespace Server.Test;

public class TestsFixture : IDisposable
{
    public readonly FirefoxDriver driver;

    public TestsFixture ()
    {
        new DriverManager().SetUpDriver(new FirefoxConfig());
        FirefoxOptions options = new FirefoxOptions();
        options.LogLevel = FirefoxDriverLogLevel.Fatal;
        options.AddArgument("--headless");

        driver = new FirefoxDriver(options);
    }

    public void Dispose()
    {
        driver.Quit();
    }
}

public class ExampleTests : IClassFixture<TestsFixture>
{
    public readonly FirefoxDriver driver;

    public ExampleTests(TestsFixture data)
    {
        driver = data.driver;
        driver.Navigate().GoToUrl("http://localhost:8000");
    }

    [Fact]
    public void EmptyTodo()
    {
        Percy.Snapshot(driver, "Empty Todo State");
    }

    //[Fact]
    public void EmptyTodoWithCss()
    {
        Percy.Snapshot(driver, "Empty Todo State", new Dictionary<string,object> {
            {"percyCSS", "#pricing {visibility: hidden;}"}
        });
    }
}
