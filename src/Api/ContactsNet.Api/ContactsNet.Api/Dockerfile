FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ContactsNet.Api/ContactsNet.Api.csproj", "ContactsNet.Api/"]
RUN dotnet restore "ContactsNet.Api/ContactsNet.Api.csproj"
COPY . .
WORKDIR "/src/ContactsNet.Api"
RUN dotnet build "ContactsNet.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactsNet.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactsNet.Api.dll"]
