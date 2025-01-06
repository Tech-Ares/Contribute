#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime
WORKDIR /app

COPY . /app
ENTRYPOINT ["dotnet", "KUX.Upload.dll"]

#����ʱ��Ϊ�Ϻ�ʱ��
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone