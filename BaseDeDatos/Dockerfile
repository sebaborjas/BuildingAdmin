FROM mcr.microsoft.com/mssql/server:2017-latest

ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=MyPass@word
ENV MSSQL_PID=Developer
ENV MSSQL_USER=SA

RUN mkdir -p /Backups

COPY levantarBase.sh /Backups/
COPY Backup.bak /Backups/

RUN chmod +x /Backups/levantarBase.sh

EXPOSE 1433

ENTRYPOINT ["/opt/mssql/bin/sqlservr"]

