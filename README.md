# AngularWebApiDDD
Asp.net core 6 Web Api backend with DDD pattern and Angular Front End

INSTRUCTIONS FOR RUN THE SAMPLE IN THE BACKEND FOLDER

1) Clone or download the Repository in a local folder, the project is divided into 2 folders Backend and Frontend respectively
2) Open solution found in backend folder in visual studio 2022
3) Create a database in sql server (in my case I use the express version and the database is called TASKDB)
4) In asp.net core api web project, modify the appsettings.json and appsettings.Development.json files as follows:
   "ConnectionStrings": {
    "DefaultConnection": "Data Source=[ip_server],1433;Database=[databse_name];User Id=[user];Password=[password];"
  },
  change the connection string values that apply on your computer
  It is important to mention that since I am using docker to compile the application, the port of the sql server service that is being used had to be included in the   connection string on the server, and I also had to open the ports in the firewall for that port in in this case 1433, in my case the connection string was as follows 
  
  "Data Source=192.168.0.105\\sqlexpress,1433;Database=TASKDB;User Id=sa;Password=sa;"
  
5) Open the  Package Manager Console (Tools->Nuget Package Manager->Package Manager Console) and select the project "Infrastructure" 
   in the combobox "Default Project"
6) In Console run command "add-migration Initial" (This creates the script to create the database)
7) In Console run command "update-database" (This creates the database on the sql server)
8) To generate the compilation of the project it is necessary to have configured the docker desktop application
9) Open the console in the same folder in which the solution project is hosted "TEST.sln"
10) In console execute the following command for generate the image of the web api, After this the image should be reflected in the image list in docker Desktop

   docker image build -t asptasks:1.0 -f .\Api\Dockerfile .
   
 11) In console execute the following command for generate the container of the applicaton 

   docker container create -name task-container -p 2023:80 asptasks:1.0
   
   this command create the container and after this should be reflected in the container list in docker Desktop
 
 12) In the docker desktop Run the container 
 13) in the browser or using postman verify that the rest service is running (http://localhost:2023/api/task/select) 
 
 the service should be enabled to be used in the angular application
 
 INSTRUCTIONS FOR RUN THE SAMPLE IN THE FRONTEND FOLDER
 
 1) In the console inside the frontend folder run the following command to install the project dependencies
   
    npm install --force
    
    The -f or --force argument will force npm to get remote resources even if a local copy exists on disk and will also force resolution of dependencies
 
 2) Once the previous process is finished, execute the following command in the console to execute the Angular application

    ng serve
    
 3) in the browser open the following direction http://localhost:4200, 
    
    Once these steps are completed, the angular app will be available at this address, in the left menu, select the tasks option where you can create new task records, do filters, pagination, etc.


     

