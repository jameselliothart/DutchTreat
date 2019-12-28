# Dutch Treat

## Dev Environment

Pull and run mssql docker container. `-u 0:0` gives root privileges.

```sh
$ docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=mssql-dt1' -u 0:0 -p 1433:1433 --name sql1 -v /home/james/dockervolumes/DutchTreat:/var/opt/mssql -d mcr.microsoft.com/mssql/server
$ docker exec -it sql1 "bash"
# /opt/mssql-tools/bin/sqlcmd -S localhost -U sa
```

## EF Commands

`dotnet ef database update`
`dotnet ef migrations add InitialDb`
