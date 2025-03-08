FROM node:23

ARG AGGREGATOR_API

ENV AGGREGATOR_API=$AGGREGATOR_API

COPY . /app
WORKDIR /app

RUN npm i 
RUN npm run build

CMD ["npm", "start"]