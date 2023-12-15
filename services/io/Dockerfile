
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

WORKDIR /app

COPY main.cs .

RUN dotnet publish -c Release -o out main.cs

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS runtime
WORKDIR /app

# Copie des fichiers publiés depuis l'image de compilation
COPY --from=build /app/out .

# Commande par défaut à exécuter
CMD ["dotnet", "main.dll"]

