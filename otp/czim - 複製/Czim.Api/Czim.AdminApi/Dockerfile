#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["KUX.AdminApi/Czim.AdminApi.csproj", "KUX.AdminApi/"]
#COPY ["KUX.Controllers.Admin/KUX.Controllers.Admin.csproj", "KUX.Controllers.Admin/"]
#COPY ["KUX.Services.Admin/KUX.Services.Admin.csproj", "KUX.Services.Admin/"]
#COPY ["KUX.Services/KUX.Services.csproj", "KUX.Services/"]
#COPY ["KUX.Repositories/KUX.Repositories.csproj", "KUX.Repositories/"]
#COPY ["KUX.Repositories.Redis/KUX.Repositories.Redis.csproj", "KUX.Repositories.Redis/"]
#COPY ["KUX.Infrastructure/KUX.Infrastructure.csproj", "KUX.Infrastructure/"]
#COPY ["KUX.RepositoriesCore/KUX.Repositories.Core.csproj", "KUX.RepositoriesCore/"]
#COPY ["KUX.Models/KUX.Models.csproj", "KUX.Models/"]
#COPY ["KUX.Controllers/KUX.Controllers.csproj", "KUX.Controllers/"]
#RUN dotnet restore "KUX.AdminApi/Czim.AdminApi.csproj"
#COPY . .
#WORKDIR "/src/KUX.AdminApi"
#RUN dotnet build "Czim.AdminApi.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Czim.AdminApi.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Czim.AdminApi.dll"]
#

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime
WORKDIR /app

COPY . /app
ENTRYPOINT ["dotnet", "Czim.AdminApi.dll"]

#设置时间为上海时间
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
