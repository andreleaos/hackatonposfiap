#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/queue/hackatonposfiap.producer/hackatonposfiap.producer.csproj", "src/queue/hackatonposfiap.producer/"]
RUN dotnet restore "src/queue/hackatonposfiap.producer/hackatonposfiap.producer.csproj"
COPY . .
WORKDIR "/src/src/queue/hackatonposfiap.producer"
RUN dotnet build "hackatonposfiap.producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "hackatonposfiap.producer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "hackatonposfiap.producer.dll"]