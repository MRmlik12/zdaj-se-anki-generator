FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build

WORKDIR /build

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:9.0-alpine AS runtime

WORKDIR /app

COPY --from=build /build/out .

CMD [ "./ZdajseAnkiGenerator" ]