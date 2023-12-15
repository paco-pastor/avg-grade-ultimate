FROM mono:6.12.0.182-slim

WORKDIR /app

RUN apt update
RUN apt install -y mono-mcs

COPY main.cs .
RUN mcs main.cs

CMD ["sleep", "infinity"]
