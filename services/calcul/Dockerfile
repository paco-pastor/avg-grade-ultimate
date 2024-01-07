FROM python:3.12-alpine

WORKDIR /app

COPY main.py /app/

RUN pip install Flask

EXPOSE 5001

CMD ["python", "main.py"]
