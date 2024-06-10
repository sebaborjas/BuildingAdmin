#!/bin/bash
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P MyPass@word -Q "BACKUP LOG [BuildingAdmin] TO DISK = './BuildingAdmin_TailLogBackup.trn' WITH NORECOVERY"
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P MyPass@word -Q "RESTORE DATABASE [BuildingAdmin] FROM DISK = N'/Backups/Backup.bak' WITH REPLACE"