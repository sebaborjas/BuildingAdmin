#!/bin/bash

# Comprueba si se ha proporcionado un argumento
if [ -z "$1" ]; then
    echo "Uso: $0 <archivo_de_respaldo.bak>"
    exit 1
fi

# Asigna el argumento a una variable
BACKUP_FILE=$1

# Ejecuta los comandos de SQL utilizando el archivo de respaldo proporcionado
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P MyPass@word -Q "BACKUP LOG [BuildingAdmin] TO DISK = './BuildingAdmin_TailLogBackup.trn' WITH NORECOVERY"
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P MyPass@word -Q "RESTORE DATABASE [BuildingAdmin] FROM DISK = N'/Backups/$BACKUP_FILE' WITH REPLACE"
