# PhotinoStaticFileServer

Using [Photino.io ](https://www.tryphotino.io/)  allows us to create cross platform desktop applications using HTML, javascript, and CSS with C# as your backend.

This little project shows how to embed the html, script, css, and other asset files so you can produce a single exe. This way your assets won't be just loose in the app directory and users can’t edit them.

In this example we will be using the Photino.NET.Server nuget package. This will create a dot net web server which will serve up our HTML, script, and CSS files Embedded in our exe.

## The How
### 1. Create a new project

Start by creating a new simple Console Application. I’m using DotNet 8.0, but you can use 7.0 if you prefer. I also ticked the “Do not use top-level statements” under Additional Information, but you don’t have to.

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/6480b966-6111-4545-b96f-3026f0c5ed15)

### 2. Add Nuget packages.

We need the following 3 nuget package:

-Photino.NET

-Photino.NET.Server

-Microsoft.Extensions.FileProviders.Embedded

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/dc76e981-a4b4-4c9f-b154-2f10a8081fea)

### 3. Add Resource Folder

Photino.NET.Server is looking for a folder called ‘Resources’. We have to have this. We also need another folder inside this Resources folder, but we can name this folder what we want. In this case, I named it ‘Assets’ and we will see further down how to tell the file server about our folder.

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/0fe64740-588b-43ce-a20b-8e0077aba1dc)

### 4. Edit Project File

The simplest way to embed our files is to edit the project file manually. We also have a few other items to edit in the project file while we are at it. Start by right clicking the project and then click Edit Project File.

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/4926ffc8-70cb-47b5-9121-5ee252668034)

4.1. Change the OutputType from ‘Exe’ to ‘WinExe’.

If left as Exe, then we will get a console window showing up which can be useful for debugging, but we don’t want this when we run our app normally. See image below.

4.2. Add ‘GenerateEmbeddedFilesManifest’ and set it to true.

Photino.NET.Server uses the [ManifestEmbeddedFileProvider](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/file-providers?view=aspnetcore-8.0#manifest-embedded-file-provider) to access files embedded in our exe. This option will generate the needed Manifest for the file provider to use. We also need to specify what we want to Embed. See image below.

4.3. Specify Embedded Resource Location.

You can right click on a file and go to its properties and set it as an embedded resource there, but it’s easier to specify it in the project file using a folder with a wildcard. This way anything in that folder will be embedded.

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/790013e1-dc83-429f-9fbb-3c7933c6184f)


### 6. Add Assets.
Time to add your HTML, script, and CSS files. You can create your own or use the files in this repository.

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/30284004-425f-4414-acb9-7239f1fef940)


### 7. Add script and css to the HTML file.
Because we are using the file server, we can specify our script and css files in our html file like we would normally when building a web app.

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/542ef655-476d-414f-8c9e-1e9c53c22065)


### 8. Setup Server
Now we can add some C# code and we start by setting up our server. Note that here we specify that the Server uses our ‘Assets’ folder we created. Again, you can name this folder what you like as long as it is within the ‘Resources’ folder.

The server will try and find an available port, but you can tell it where to start looking by specifying the ‘startPort’ and how far it should search by specifying the ‘portRange’.

The last parameter is the baseURL. Once the server finds an available port, it will be contained in this baseURL field and will need to use this later when we create our Photino Window. This baseURL field will hold something like: http://localhost:8000

Usings:

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/0798b5c7-63a3-4066-b8d5-51dc929a27f2)

Server:

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/45bfd02a-bf84-492e-8057-83c13c9d97e7)


### 9. Setup Window
Now we can set up our window and specify what our window should be loading using the ‘baseURL’ and whatever our html file is called.

In this example we are also registering a handler for the WindowCreated event and also for the WebMessageReceived event. 

WindowCreate is just used to maximise the window as setting it during window creation is bugged in version 2.5.2 of Photino, but it will be fixed in the next version.

WebMessageReceived is what will handle any calls we make from javascript on the page to our C# backend. In our C# handler we are also sending a message back to our page using the SendWebMessage method on our Window object. 

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/f421b8e2-cd90-4081-9bc4-3309bc82386b)

You can send a message from our page using the following;

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/0c75ed6a-59c7-4bb5-ae11-46d9f0cfd4d9)

Messages from C# will be handled in our page by registering a handler in our javascript.

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/488c0523-7325-461c-b314-49cc6ffe9b01)


### 10. Test it

The app should run now if you hit F5 in Visual Studio. When the window first opens it will show an alert. Close the alert and click the button to send a message to the C# code. The C# code will just send the message straight back and you will see another alert.


### 11. Publish it

Right click the project and select Publish. 

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/95546963-65bc-4504-8a7e-25b570e896bf)

A new window will open up. Select Folder, click Next, select Folder again, click Next again. Now click the Browse button and choose where you would like to publish the files. Click Finish, then Close. You should see the below image…

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/96cba492-153b-4843-8d11-9580d728c00e)

Now click Show all settings and copy the image below. 

Deployment mode = Self-contained. This means you won’t need to install .net on the system you want to run the app on as it will contain all it needs to do so on it’s own.

Target Runtime = win-x64. This needs to match the OS you wish to run the app on. I’m on Windows 10 x64 so I need to select win-x64.

Produce single file = true. So we create a single exe file.

Trim unused code is optional.

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/8706b89e-1e39-47de-9245-e8b337526f2f)

Click save and then click the big Publish button at the top of the screen. Once done you should see the following 4 files in your publish folder.

![image](https://github.com/Nicks182/PhotinoStaticFileServer/assets/13113785/0787883b-b0c3-4d1e-8222-c315ae2e45e7)

Yes… it’s not really a single file. Depending on the dll, it can’t always be put into a single file. In this case I believe it’s because they are C++. The .pdb file is not needed to run the app though.

Double click the exe to run the app.

NOTE: For some reason the app will create our ‘Assets’ folder when running, but none of our web files will be in there.

## Additional info
I often end up having a lot of small individual script and style files which I end up bundling together.

To bundle your files you can use something like: [BundlerMinifier](https://github.com/madskristensen/BundlerMinifier)

I’m currently using a more up to date version: [BundlerMinifierPlus](https://github.com/salarcode/BundlerMinifierPlus)

