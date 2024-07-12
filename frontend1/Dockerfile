# Use the official Node.js image to build and run the app
FROM node:16 AS build
WORKDIR /app

# Install dependencies
COPY package.json package-lock.json ./
RUN npm install

# Copy the remaining source code
COPY . ./

# Expose port 3000 and start the application
EXPOSE 3000
CMD ["npm", "start"]
