/*
ImageGlass.Tools - Build tools for ImageGlass
Copyright (C) 2023 DUONG DIEU PHAP
Project homepage: https://imageglass.org

MIT License
*/
using ImageGlass.Tools;

namespace DemoApp;

public partial class Form1 : Form
{
    private readonly ImageGlassTool _igTool = new();


    public Form1()
    {
        InitializeComponent();

        // ImageGlass tool events
        _ = ConnectToImageGlassAsync();
    }



    // ImageGlassTool connection
    #region ImageGlassTool connection

    private async Task ConnectToImageGlassAsync()
    {
        _igTool.ToolMessageReceived += IgTool_ToolMessageReceived;
        _igTool.ToolClosingRequest += IgTool_ToolClosingRequest;

        // start connecting to ImageGlass
        await _igTool.ConnectAsync();
    }


    private void IgTool_ToolClosingRequest(object? sender, DisconnectedEventArgs e)
    {
        Close();
    }


    private void IgTool_ToolMessageReceived(object? sender, MessageReceivedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.MessageData)) return;


        Txt.Text = $"""
            EVENT NAME = {e.MessageName}
            EVENT DATA =
            {e.MessageData}
            
            ----------------------------------------------
            {Txt.Text}
            """;
    }


    #endregion // ImageGlassTool connection


}