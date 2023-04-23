# ImageGlass.Tools - Build tools for ImageGlass
ImageGlass.Tools is a set of APIs to integrate third-party software with ImageGlass.
With ImageGlass.Tools, developers can extend the functionality of ImageGlass by integrating their own software solutions, making it a highly customizable and versatile tool for all image viewing needs.

You can download tools for ImageGlass 9 at: https://imageglass.org/tools.

![ImageGlass.Tools](https://raw.githubusercontent.com/ImageGlass/ImageGlass.Tools/main/demo.jpg)

## Getting started
Here are the simple steps to use APIs from ImageGlass.Tools. You can check out the [`DemoApp` project](./Source/DemoApp) for complete sample.

1. To get started, you need to create a new project in Visual Studio.
2. Install [ImageGlass.Tools from Nuget](https://www.nuget.org/packages/ImageGlass.Tools).
_Alternatively, you also can clone this repo and add `ImageGlass.Tools.csproj` as a reference project_.
3. Create a new instance of `ImageGlassTool`:
```cs
private readonly ImageGlassTool _igTool = new ImageGlassTool();
```

4. Add event listeners to ImageGlass:
```cs
_igTool.ToolMessageReceived += IgTool_ToolMessageReceived;
_igTool.ToolClosingRequest += IgTool_ToolClosingRequest;
```

5. Handle event from ImageGlass:
```cs
private void IgTool_ToolMessageReceived(object? sender, MessageReceivedEventArgs e)
{
  if (string.IsNullOrEmpty(e.MessageData)) return;

  if (e.MessageName == ImageGlassEvents.IMAGE_LOADED)
  {
    Trace.WriteLine("Image is loaded");
    Trace.WriteLine(e.MessageData);
  }
}
```

6. Start connecting to ImageGlass:
```cs
await _igTool.ConnectAsync();
```

## Add an external tool to ImageGlass 9
1. Open `igconfig.json` file with a text editor such as NotePad or VS Code.
2. Ensure that ImageGlass app is not running.
3. In the Tools section of the `igconfig.json` file, add the following code:
```js
// in igconfig.json
"Tools": [
  {
    "ToolId": "Tool_MyDemoApp", // a unique ID
    "ToolName": "My Demo app", // name of the tool
    "Executable": "path\\to\\the\\DemoApp.exe",
    "Argument": "<file>", // file path to pass to the tool
    "IsIntegrated": true|false // true: if the tool supports 'ImageGlass.Tools'
  }
]
```

4. To assign hotkeys to the tool, add the following code:
```js
// in igconfig.json
"MenuHotkeys": {
  "Tool_MyDemoApp": ["X", "Ctrl+E"] // press X or Ctrl+E to open/close the tool
}
```

5. Save the file, and you're done!



