FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /app/src
COPY Honlsoft.TimeLog.Console/Honlsoft.TimeLog.Console.csproj Honlsoft.TimeLog.Console/Honlsoft.TimeLog.Console.csproj
COPY Honlsoft.TimeLog/Honlsoft.TimeLog.csproj Honlsoft.TimeLog/Honlsoft.TimeLog.csproj
COPY Honlsoft.TimeLog.Markdown/Honlsoft.TimeLog.Markdown.csproj Honlsoft.TimeLog.Markdown/Honlsoft.TimeLog.Markdown.csproj
COPY Honlsoft.TimeLog.Tests/Honlsoft.TimeLog.Tests.csproj Honlsoft.TimeLog.Tests/Honlsoft.TimeLog.Tests.csproj
COPY Honlsoft.TimeLog.sln .
RUN dotnet restore

COPY . .

RUN dotnet test

WORKDIR /app/src/Honlsoft.TimeLog.Console
RUN dotnet build -c Release -o /app/bin

FROM mcr.microsoft.com/dotnet/runtime:6.0
COPY --from=build /app/bin /app/bin

# Just in case we need to double check the output.
RUN ls -l /app/bin

WORKDIR /app/logs
VOLUME /app/logs


ENTRYPOINT ["/app/bin/hs-time-log"]
CMD ["--help"]