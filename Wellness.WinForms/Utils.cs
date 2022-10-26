namespace Wellness.WinForms;

public static class Utils
{
    public static string GetScreenKey(Screen screen) => $"{screen.DeviceName}{screen.Bounds}";
}
