FROM mcr.microsoft.com/mssql/server:2022-latest
USER root
RUN mkdir -p /usr/config

COPY ./config /usr/config
COPY ./data /docker-entrypoint-initdb.d/

# Grant permissions for to our scripts to be executable
RUN chmod +x /usr/config/entrypoint.sh
RUN chmod +x /usr/config/configure-db.sh

ENTRYPOINT ["./usr/config/entrypoint.sh"]