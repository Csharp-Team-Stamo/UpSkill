#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ClientSide/UpSkill.ClientSide.csproj", "ClientSide/"]
RUN dotnet restore "ClientSide/UpSkill.ClientSide.csproj"
COPY . .
WORKDIR "/src/ClientSide"
RUN dotnet build "UpSkill.ClientSide.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UpSkill.ClientSide.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UpSkill.ClientSide.dll"]