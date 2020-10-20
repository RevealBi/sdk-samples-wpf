Sample showing how to use OneDrive as a data source for Excel and CSV files.
The sample is based on the application you can generate following the instructions here: https://docs.microsoft.com/en-us/azure/active-directory/develop/tutorial-v2-windows-desktop
It uses Microsoft.Identity.Client library to authenticate to OneDrive and obtain the required token.

Please note you need to modify the file App.xaml.cs first and set the ClientId for your OneDrive application, if not the application will fail to start.
