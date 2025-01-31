@echo off
docker rm -f wallet-db
docker rmi wallet-db
docker build . -t wallet-db
docker run -d -p 5432:5432 --name wallet-db wallet-db