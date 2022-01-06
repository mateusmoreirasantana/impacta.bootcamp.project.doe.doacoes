#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["impacta.bootcamp.project.doe.doacoes.api/impacta.bootcamp.project.doe.doacoes.api.csproj", "impacta.bootcamp.project.doe.doacoes.api/"]
COPY ["impacta.bootcamp.project.doe.doacoes.core/impacta.bootcamp.project.doe.doacoes.core.csproj", "impacta.bootcamp.project.doe.doacoes.core/"]
COPY ["impacta.bootcamp.project.doe.doacoes.infra.data/impacta.bootcamp.project.doe.doacoes.infra.data.csproj", "impacta.bootcamp.project.doe.doacoes.infra.data/"]
COPY ["impacta.bootcamp.project.doe.doacoes.ioc/impacta.bootcamp.project.doe.doacoes.ioc.csproj", "impacta.bootcamp.project.doe.doacoes.ioc/"]
COPY ["impacta.bootcamp.project.doe.doacoes.application/impacta.bootcamp.project.doe.doacoes.application.csproj", "impacta.bootcamp.project.doe.doacoes.application/"]
RUN dotnet restore "impacta.bootcamp.project.doe.doacoes.api/impacta.bootcamp.project.doe.doacoes.api.csproj"
COPY . .
WORKDIR "/src/impacta.bootcamp.project.doe.doacoes.api"
RUN dotnet build "impacta.bootcamp.project.doe.doacoes.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "impacta.bootcamp.project.doe.doacoes.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "impacta.bootcamp.project.doe.doacoes.api.dll"]
