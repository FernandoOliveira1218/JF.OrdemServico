FROM node:20-alpine AS builder
WORKDIR /app
COPY ./src/Presentations/jf.ordemservico.web ./
RUN npm install && npm run build
EXPOSE 3000
CMD ["npm", "run", "dev"]