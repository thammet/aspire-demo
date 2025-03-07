FROM mcr.microsoft.com/mssql/server:2022-latest
USER root
RUN mkdir -p /usr/config

WORKDIR /usr/config

COPY ./config .
COPY ./data /docker-entrypoint-initdb.d/


#RUN ls

# Grant permissions for to our scripts to be executable
# RUN chmod +x /usr/config/entrypoint.sh
# RUN chmod +x /usr/config/configure-db.sh

RUN chmod +x entrypoint.sh
RUN chmod +x configure-db.sh

RUN ls

ENTRYPOINT ["/entrypoint.sh"]