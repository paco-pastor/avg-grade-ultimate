FROM mono:6.12.0.182-slim

WORKDIR /app

RUN apt update
RUN apt install -y mono-mcs
RUN apt install -y nuget

RUN nuget install Newtonsoft.Json
RUN nuget install CsvHelper

COPY main.cs .
RUN mcs -r:Newtonsoft.Json.dll -r:CsvHelper.dll main.cs # TODO

CMD ["sleep", "infinity"]
