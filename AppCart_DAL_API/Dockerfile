#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["AppCart_DAL_API/AppCart_DAL_API.csproj", "AppCart_DAL_API/"]
RUN dotnet restore "AppCart_DAL_API/AppCart_DAL_API.csproj"
COPY . .
WORKDIR "/src/AppCart_DAL_API"
RUN dotnet build "AppCart_DAL_API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AppCart_DAL_API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AppCart_DAL_API.dll"]