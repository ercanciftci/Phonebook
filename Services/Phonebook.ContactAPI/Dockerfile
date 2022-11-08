FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/Phonebook.Shared/*.csproj Shared/Phonebook.Shared/
COPY Services/Phonebook.ContactAPI/*.csproj Services/Phonebook.ContactAPI/
RUN dotnet restore Services/Phonebook.ContactAPI/*.csproj
COPY . ./
RUN dotnet publish Services/Phonebook.ContactAPI/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Phonebook.ContactAPI.dll" ]