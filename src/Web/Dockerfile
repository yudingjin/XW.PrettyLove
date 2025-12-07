FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY Lib/Release/publish /app
EXPOSE 80
ENV TZ="Asia/Shanghai"
ENTRYPOINT ["dotnet", "XW.PrettyLove.Web.Front.dll"]
