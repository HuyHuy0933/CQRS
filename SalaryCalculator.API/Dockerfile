#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Services/SalaryCalculator/SalaryCalculator.API/SalaryCalculator.API.csproj", "src/Services/SalaryCalculator/SalaryCalculator.API/"]
COPY ["src/Services/SalaryCalculator/SalaryCalculator.Infra.Persistent/SalaryCalculator.Infra.Persistent.csproj", "src/Services/SalaryCalculator/SalaryCalculator.Infra.Persistent/"]
COPY ["src/Services/SalaryCalculator/SalaryCalculator.Application/SalaryCalculator.Application.csproj", "src/Services/SalaryCalculator/SalaryCalculator.Application/"]
COPY ["src/Services/SalaryCalculator/SalaryCalculator.Core/SalaryCalculator.Core.csproj", "src/Services/SalaryCalculator/SalaryCalculator.Core/"]
RUN dotnet restore "src/Services/SalaryCalculator/SalaryCalculator.API/SalaryCalculator.API.csproj"
COPY . .
WORKDIR "/src/src/Services/SalaryCalculator/SalaryCalculator.API"
RUN dotnet build "SalaryCalculator.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalaryCalculator.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalaryCalculator.API.dll"]