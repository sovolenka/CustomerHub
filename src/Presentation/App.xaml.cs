﻿using System.Windows;
using System.Windows.Threading;
using Serilog;

namespace Presentation;

public partial class App : Application
{
    public App()
    {
        ConfigureLogger();
    }
    
    private void ConfigureLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
    
    private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        Log.Error(e.Exception, "Unhandled exception");
        e.Handled = true;
    }
    
    private void App_OnExit(object sender, ExitEventArgs e)
    {
        Log.Information("Application closed");
    }
}
