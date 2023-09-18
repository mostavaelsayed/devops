FROM ontellus.azurecr.io/node:18.17.0-alpine AS base

# Install dependencies only when needed
FROM base AS deps
# Check https://github.com/nodejs/docker-node/tree/b4117f9333da4138b03a546ec926ef50a31506c3#nodealpine to understand why libc6-compat might be needed.
RUN apk add --no-cache libc6-compat
WORKDIR /app

# Install dependencies based on the preferred package manager
COPY --link ./app/package*.json ./

RUN npm ci


# Rebuild the source code only when needed
FROM base AS builder

WORKDIR /app
COPY --from=deps /app/node_modules ./node_modules
COPY ./app .

# Next.js collects completely anonymous telemetry data about general usage.
# Learn more here: https://nextjs.org/telemetry
# Uncomment the following line in case you want to disable telemetry during the build.
ENV NEXT_TELEMETRY_DISABLED 1

# https://jestjs.io/docs/cli#--watch
# https://jestjs.io/docs/cli#--watchall
# https://jestjs.io/docs/cli#--runinband
# RUN npm run test -- --coverage --ci --watchAll=false --silent --runInBand

RUN npm run build

# Production image, copy all the files and run next
FROM ontellus.azurecr.io/node:18.17.0-alpine AS runner

RUN apk add --no-cache bash

WORKDIR /app

ENV NODE_ENV production

# Uncomment the following line in case you want to disable telemetry during runtime.
ENV NEXT_TELEMETRY_DISABLED 1

RUN adduser --disabled-password \
  --home /app \
  --gecos '' appuser && chown -R appuser /app

COPY --from=builder /app/public ./public

# Set the correct permission for prerender cache
RUN mkdir .next

# Automatically leverage output traces to reduce image size
# https://nextjs.org/docs/advanced-features/output-file-tracing
COPY --from=builder --chown=appuser:appuser /app/.next/standalone ./
COPY --from=builder --chown=appuser:appuser /app/.next/static ./.next/static

RUN chown -R appuser:appuser /app && \
    chown -R appuser:appuser /app/.next && \
    chown -R appuser:appuser /app/.next/static && \
    chown -R appuser:appuser /app/public

USER appuser

EXPOSE 3001

ENV PORT 3001
ENV HOSTNAME "0.0.0.0"

ENV scripts_path=/app/public/scripts

CMD ["/bin/bash", "-c", "node server.js"]