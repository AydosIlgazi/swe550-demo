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
        Percy.Snapshot(driver, "Index");
    }

    [Fact]
    public void EmptyTodoWithCss()
    {
        Percy.Options snapshotOptions = new Percy.Options();
        snapshotOptions.Add("percyCSS", "#pricing {visibility: hidden;}");
        snapshotOptions.Add("widths", new [] { 600, 1200 });
        Percy.Snapshot(driver, "Index with Percy Css", snapshotOptions);
    }
}
