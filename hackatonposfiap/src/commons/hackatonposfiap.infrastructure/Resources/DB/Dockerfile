FROM mysql:latest

ENV MYSQL_ROOT_PASSWORD=abc@123
ENV MYSQL_DATABASE=hackatondb
ENV MYSQL_USER=hackaton_user
ENV MYSQL_PASSWORD=abc@123

COPY init.sql /docker-entrypoint-initdb.d/

EXPOSE 3306