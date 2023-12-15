using PhotinoNET;
using PhotinoNET.Server;
using System.Reflection;

namespace PhotinoStaticFileServer
{
    /*
     * Note the changes in the project file...
        <ItemGroup>
		    <EmbeddedResource Include="Assets\**\*" />
	    </ItemGroup>
     */

    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {

            PhotinoServer
            .CreateStaticFileServer(args: args, startPort: 8000, portRange: 100, webRootFolder: "Assets", out string baseUrl)
            .RunAsync();

            PhotinoWindow Wind = new PhotinoWindow();

            Wind.Load($"{baseUrl}/index.html");

            #region Addition functionality
            Wind.RegisterWindowCreatedHandler(Win_WindowCreated);

            Wind.RegisterWebMessageReceivedHandler((object sender, string message) =>
            {
                var window = (PhotinoWindow)sender;

                // Send a message back the to JavaScript event handler.
                // "window.external.receiveMessage(callback: Function)"
                window.SendWebMessage("C# reveived the following message from the browser: " + message);
            });
            #endregion Addition functionality

            Wind.WaitForClose();
        }

        private static void Win_WindowCreated(object? sender, EventArgs e)
        {
            // Using this event seems to be the only way to get Maximized to work
            // Trying to set it before the window is created seems to not work...
            (sender as PhotinoWindow).SetMaximized(true);
        }


    }
}
