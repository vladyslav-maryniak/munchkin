FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5140

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Munchkin.API/Munchkin.API.csproj", "Munchkin.API/"]
COPY ["Munchkin.Domain/Munchkin.Domain.csproj", "Munchkin.Domain/"]
COPY ["Munchkin.Shared/Munchkin.Shared.csproj", "Munchkin.Shared/"]
COPY ["Munchkin.Application/Munchkin.Application.csproj", "Munchkin.Application/"]
RUN dotnet restore "Munchkin.API/Munchkin.API.csproj"
COPY . .
WORKDIR "/src/Munchkin.API"
RUN dotnet build "Munchkin.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Munchkin.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5140
ENTRYPOINT ["dotnet", "Munchkin.API.dll"]