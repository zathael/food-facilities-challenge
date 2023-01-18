#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FoodFacilities.Api.csproj", "."]
RUN dotnet restore "./FoodFacilities.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "FoodFacilities.Api.csproj" -c Release -o /app/build

FROM build AS publish

### Post-Mortem Bugifx: ADD CSV TO DOCKER FILE
run mkdir "bin/Release/net7.0/DAL/v1" -p
RUN cp "DAL/v1/Mobile_Food_Facility_Permit.csv" "bin/Release/net7.0/DAL/v1/Mobile_Food_Facility_Permit.csv"
### DONE ADDING CSV TO DOCKER FILE

RUN dotnet publish "FoodFacilities.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# dev note: Did you know that this dockerfile can service a library, and export it via NPM?
#			You can even choose the language.
# RUN swagger tofile --output "FoodFacilities/generated/swagger.json" "FoodFacilities.Api/bin/Debug/net7.0/.dll" v1 generates your needed swagger.json file
# Ask me more if you're curious as this is not in scope & is not designed to work if you just uncomment it

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodFacilities.Api.dll"]