
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime
WORKDIR /app

COPY . /app
ENTRYPOINT ["dotnet", "Czim.AppApi.dll"]

#����ʱ��Ϊ�Ϻ�ʱ��
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
