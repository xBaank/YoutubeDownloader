﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeDownloader.Services;
using YoutubeDownloader.ViewModels.Framework;

namespace YoutubeDownloader.ViewModels.Dialogs;

public class SettingsViewModel : DialogScreen
{
    private readonly SettingsService _settingsService;
    private readonly IViewModelFactory _viewModelFactory;
    private readonly DialogManager _dialogManager;

    public SettingsViewModel(SettingsService settingsService, IViewModelFactory viewModelFactory, DialogManager dialogManager)
    {
        _settingsService = settingsService;
        _viewModelFactory = viewModelFactory;
        _dialogManager = dialogManager;
    }

    public bool IsAutoUpdateEnabled
    {
        get => _settingsService.IsAutoUpdateEnabled;
        set => _settingsService.IsAutoUpdateEnabled = value;
    }

    public bool IsDarkModeEnabled
    {
        get => _settingsService.IsDarkModeEnabled;
        set => _settingsService.IsDarkModeEnabled = value;
    }

    public bool ShouldInjectTags
    {
        get => _settingsService.ShouldInjectTags;
        set => _settingsService.ShouldInjectTags = value;
    }

    public bool ShouldSkipExistingFiles
    {
        get => _settingsService.ShouldSkipExistingFiles;
        set => _settingsService.ShouldSkipExistingFiles = value;
    }

    public string FileNameTemplate
    {
        get => _settingsService.FileNameTemplate;
        set => _settingsService.FileNameTemplate = value;
    }

    public int ParallelLimit
    {
        get => _settingsService.ParallelLimit;
        set => _settingsService.ParallelLimit = Math.Clamp(value, 1, 10);
    }
    
    public async Task Login()
    {
        Close();
        await _dialogManager.ShowDialogAsync(
            _viewModelFactory.CreateBrowserSettingsViewModel()
        );
    }
    
    public void Logout()
    {
        _settingsService.Cookies = new Dictionary<string, string>();
        _settingsService.PageId = null;
        Refresh();
    }
    
    public bool IsLogged => _settingsService.Cookies.Any();
    public bool IsNotLogged => !IsLogged;
}