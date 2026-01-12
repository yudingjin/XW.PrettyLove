dotnet publish -c Release -r linux-x64 --self-contained false XW.PrettyLove.Web.Front/XW.PrettyLove.Web.Front.csproj -o Lib/Release/publish
del Lib\Release\publish\*.pdb

docker build -t harbor.lingshumed.com/test/love_service:v1.1.14  -f Dockerfile .
docker login harbor.lingshumed.com -u admin -p admin@123
docker push harbor.lingshumed.com/test/love_service:v1.1.14
pause
