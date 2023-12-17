FROM node:21-alpine3.18

WORKDIR /app

COPY ./ .

RUN cd ./back && npm install

EXPOSE 3000

CMD [ "node", "./back/index.js" ]
