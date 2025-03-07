FROM mcr.microsoft.com/mssql/server:2022-latest
USER root
RUN mkdir -p /usr/config

WORKDIR /usr/config

COPY ./config .
COPY ./data /docker-entrypoint-initdb.d/

# Grant permissions for to our scripts to be executed
RUN chmod +x entrypoint.sh
RUN chmod +x configure-db.sh

ENTRYPOINT ["./entrypoint.sh"]