# MYSQL-.NET-Data-Manipulation-with-UI
To run, first we need the Library you can find here: https://github.com/achoda3/FSharp-MySQL-10-Tasks-Library

Now import the project into Visual Studio. 
Second, add the Library imported above by right clicking your solution and pressing "Add" then "Existing Project" and then selecting the .fsproj of our library.

Also, if both folders are in the same directory, no other changes need to be made, but if they aren't, update the Proj5.csproj to have the correct relative directory to the library

Lastly, we need to have a MySQL sserver running with the relative data. The data we currently use can be found in a csv dataSmall in bin/Debug/netcoreapp3.1, however, based on that format, you can build your own data set. 

Now you can run the server. The UI is relatively simple and the commands can be run based on the csv for the F# commands and the SQL database for the SQL commands.

By the end, your output will looks like this:

![image](https://user-images.githubusercontent.com/60198023/147705312-b43f7a48-2672-4c1d-b1a1-3a35c6a7a1be.png)

![image](https://user-images.githubusercontent.com/60198023/147705388-e65f19d2-accc-46f8-b9c8-e4cbc127d1ba.png)
