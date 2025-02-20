﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using TgDownloaderCore.Models;
using TgDownloaderCore.Utils;
using TgLocaleCore.Utils;
using TgStorageCore.Helpers;

AppModel app;
MenuHelper menu = MenuHelper.Instance;
TgLocaleHelper locale = TgLocaleHelper.Instance;
TgLogHelper log = TgLogHelper.Instance;
TgStorageHelper tgStorage = TgStorageHelper.Instance;

Setup();

do
{
    try
    {
        menu.ShowTableMain();
        string userChoose = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title(locale.MenuSwitchNumber)
            .PageSize(10)
            .MoreChoicesText(locale.MoveUpDown)
            .AddChoices(locale.MenuMainExit, locale.MenuMainStorage, locale.MenuMainClient, locale.MenuMainDownload));
        switch (userChoose)
        {
            case "Exit":
                menu.Value = MenuMain.Exit;
                break;
            case "Storage settings":
                menu.Value = MenuMain.SetStorage;
                menu.SetupStorage();
                break;
            case "Client settings":
                menu.Value = MenuMain.SetClient;
                menu.SetupClient();
                break;
            case "Download settings":
                menu.Value = MenuMain.SetDownload;
                menu.SetupDownload();
                break;
        }
    }
    catch (Exception ex)
    {
        log.Line(locale.StatusException + log.GetMarkupString(ex.Message));
        if (ex.InnerException is not null)
            log.Line(locale.StatusInnerException + log.GetMarkupString(ex.InnerException.Message));
        menu.Value = MenuMain.Exit;
    }
} while (menu.Value is not MenuMain.Exit);

void Setup()
{
    // App.
    app = AppUtils.LoadSettings();
    app.SetVersion(Assembly.GetExecutingAssembly());
    if (!app.IsExistsStoragePath)
    {
        app.SetStoragePath(Path.Combine(Directory.GetCurrentDirectory(), FileNameUtils.Storage));
        AppUtils.StoreSettings(app);
    }
    tgStorage.FileName = app.StoragePath;
    // Console.
    Console.OutputEncoding = Encoding.UTF8;
    log.SetMarkupLineStamp(AnsiConsole.MarkupLine);
    // Storage.
    tgStorage.CreateOrConnectDb();
    // Client.
    menu.App = app;
    menu.ClientConnectExists();
}