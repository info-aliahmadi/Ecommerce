
Run the all containers :

docker-compose up -d
http://localhost:9200
http://localhost:5601

Test the redis container :


Add-Migration dbVersion_1 -Context ApplicationDbContext -StartupProject Hydra.Web

Update-Database -Context ApplicationDbContext -verbose -StartupProject Hydra.Web


Remove-Migration -Context ApplicationDbContext -StartupProject Hydra.Web




===============================================================================

Update-Database dbVersion_03 -Context ApplicationDbContext -verbose

Remove-Migration -Context ApplicationDbContext -StartupProject Hydra.Web


-- remove migration
Update-Database -Migration 0 -Context ApplicationDbContext  -StartupProject Hydra.Web
Remove-Migration -Context ApplicationDbContext -StartupProject Hydra.Web


