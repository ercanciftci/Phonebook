FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Phonebook.Shared/*.csproj Shared/Phonebook.Shared/
COPY Services/Phonebook.ReportAPI/*.csproj Services/Phonebook.ReportAPI/
RUN dotnet restore Services/Phonebook.ReportAPI/*.csproj
COPY . .
RUN dotnet publish Services/Phonebook.ReportAPI/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Phonebook.ReportAPI.dll" ]